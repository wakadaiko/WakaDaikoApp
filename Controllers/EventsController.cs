using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
// using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

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
            string? pagination = Request.Query["pagination"];
            string? search = Request.Query["search"];
            string? status = Request.Query["status"];
            string? alphabet = Request.Query["alphabet"];
            string? date = Request.Query["date"];

            Console.WriteLine($"\n\n\n\n{pagination}\n\n\n\n");
            Console.WriteLine($"\n\n\n\n{search}\n\n\n\n");
            Console.WriteLine($"\n\n\n\n{status}\n\n\n\n");
            Console.WriteLine($"\n\n\n\n{alphabet}\n\n\n\n");
            Console.WriteLine($"\n\n\n\n{date}\n\n\n\n");

            return View("Index", GetEvents(0, pagination ?? "", search ?? "", status ?? "", alphabet ?? "", date ?? ""));
        }

        [HttpGet("/events/{id}")]
        public IActionResult Index(int id) => View(GetEvents(id, "1"));

        // Functions

        public List<Event> GetEvents(int id = 0, string pagination = "", string search = "", string status = "", string alphabet = "", string date = "")
        {
            // Variables

            _ = int.TryParse(pagination, out int pagination2);

            // Initial load

            List<Event> events = _repository
                .GetEvents()
                .OrderByDescending(e => e.Date)
                // .Skip(pagination2)
                // .Take(9)
                .ToList();

            // ID

            if (id > 0) events = [.. events.Where(e => e.EventId == id)];

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
