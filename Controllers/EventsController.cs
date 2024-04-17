using Microsoft.AspNetCore.Mvc;
using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;

namespace WakaDaikoApp.Controllers
{
    // public class EventsController(IRepository r, UserManager<AppUser> u) : Controller
    public class EventsController(IRepository r) : Controller
    {
        // Variables
        readonly IRepository _repository = r;

        // readonly UserManager<AppUser> _userManager = u;

        // Controllers & routes
        public IActionResult Index()
        {
            string? search = Request.Query["search"];
            string? status = Request.Query["status"];
            string? alphabet = Request.Query["alphabet"];
            string? date = Request.Query["date"];

            return View("Index", GetEvents("", "", search ?? "", status ?? "", alphabet ?? "", date ?? ""));
        }

        [HttpGet("/events/view/{eventDesc}")]
        public IActionResult Index(string eventDesc) => View(GetEvents("", eventDesc));

        // Workaround - Extra parameter

        [HttpGet("/events/page/{paginationId}/")]
        public IActionResult Index(string paginationId, string z)
        {
            string? search = Request.Query["search"];
            string? status = Request.Query["status"];
            string? alphabet = Request.Query["alphabet"];
            string? date = Request.Query["date"];

            return View(GetEvents(paginationId, "", search ?? "", status ?? "", alphabet ?? "", date ?? ""));
        }

        // Functions

        public List<Event> GetEvents(string paginationId = "", string eventDesc = "", string search = "", string status = "", string alphabet = "", string date = "")
        {
            // Variables

            int EVENTS_PER_PAGE = 3;

            _ = int.TryParse(paginationId, out int paginationId2);

            // Load initial events

            List<Event> events = [
                .. _repository
                .GetEvents()
                .OrderByDescending(e => e.Date)
            ];

            // Pagination

            ViewBag.PaginationCount = events.Count / EVENTS_PER_PAGE;
            // ViewBag.PaginationId = ViewBag.PaginationCount.ToString();

            if (paginationId != "")
            {
                if (paginationId2 < 1 || paginationId2 > ViewBag.PaginationCount) events = [];
                else
                {
                    events.Reverse();
                    events = events
                    .Skip((paginationId2 - 1) * EVENTS_PER_PAGE)
                    .Take(EVENTS_PER_PAGE)
                    .Reverse()
                    .ToList();

                    ViewBag.PaginationId = paginationId;
                }
            }
            // else if (paginationId == "" && eventDesc == "") events = events.Take(4).ToList();

            // Description

            if (eventDesc.Length > 0) events = [.. events.Where(e => e.Title != null && e.Description == eventDesc)];

            // Search

            if (search != "") events = [.. events.Where(e => e.Title != null && e.Title.Contains(search))];

            // Status

            switch (status)
            {
                case "eStatusUpcoming":
                    events = [.. events.Where(e => e.Date > DateOnly.FromDateTime(DateTime.Now))];
                    break;

                case "eStatusPast":
                    events = [.. events.Where(e => e.Date < DateOnly.FromDateTime(DateTime.Now))];
                    break;
            }

            // Date

            if (date == "eDateOldest") events = [.. events.OrderBy(e => e.Date)];

            // Alphabet

            switch (alphabet)
            {
                case "eAlphabetAZ":
                    events = [.. events.OrderBy(e => e.Title)];
                    break;

                case "eAlphabetZA":
                    events = [.. events.OrderByDescending(e => e.Title)];
                    break;
            }

            // Return

            return events;
        }
    }
}
