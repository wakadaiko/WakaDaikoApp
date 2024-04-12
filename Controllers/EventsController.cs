using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
// using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WakaDaikoApp.Controllers
{
    public class EventsController(IRepository r, UserManager<AppUser> u) : Controller
    {
        // Variables
        readonly IRepository _repository = r;

        // readonly UserManager<AppUser> _userManager = u;

        // Controllers & routes

        public IActionResult Index()
        {
            string? pagination = Request.Query["pagination"];
            string? search = Request.Query["search"];
            string? status = Request.Query["status"];
            string? alphabet = Request.Query["alphabet"];
            string? date = Request.Query["date"];

            // Console.WriteLine($"\n\n\n\n{search}\n\n\n\n");
            // Console.WriteLine($"\n\n\n\n{status}\n\n\n\n");
            // Console.WriteLine($"\n\n\n\n{alphabet}\n\n\n\n");
            // Console.WriteLine($"\n\n\n\n{date}\n\n\n\n");

            return View("Index", GetEvents(pagination, 0, search ?? "", status ?? "", alphabet ?? "", date ?? ""));
        }

        [HttpGet("/events/{id}")]
        public IActionResult Index(int id) => View(GetEvents("", id));

        // Functions

        public List<Event> GetEvents(string pagination = "", int id = 0, string search = "", string status = "", string alphabet = "", string date = "")
        {
            // Initial load

            List<Event> events = _repository
                .GetEvents()
                .Select(e => e)
                .OrderByDescending(e => e.Date)
                .Take(6)
                .ToList();

            // ID

            if (id > 0)
            {
                events = [.. events.Where(e => id == e.EventId)];

                // if (events.Count < 1) Console.WriteLine("❌");
                // if (events.Count < 1) return null;
            }

            // Search

            if (search != "") events = [.. events.Where(e => e.Title != null && e.Title.Contains(search))];

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

            // Oldest

            if (date == "eDateOldest") events = [.. events.OrderBy(e => e.Date)];

            // Return

            return events;
        }
    }
}
