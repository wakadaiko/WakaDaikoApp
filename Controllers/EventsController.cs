using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace WakaDaikoApp.Controllers
{
    public class EventsController(IRepository r, UserManager<AppUser> u) : Controller
    {
        // Variables
        readonly IRepository _repository = r;

        readonly UserManager<AppUser> _userManager = u;

        // Routes

        public IActionResult Index()
        {
            // Console.WriteLine("\n\n\n\n1\n\n\n\n");

            return View(GetEvents());
        }
        // public IActionResult Index() => View((from e in _repository.GetEvents() select e).ToList());

        // [HttpPost]
        // public IActionResult Index(string search, string status, string alphabet, string date)
        // {
        //     Console.WriteLine("\n\n\n\n2\n\n\n\n");

        //     Console.WriteLine(search);
        //     Console.WriteLine(status);
        //     Console.WriteLine(alphabet);
        //     Console.WriteLine(date);

        //     return View(GetEvents(search, status, alphabet, date));
        // }

        [HttpGet("/events/{id}")]
        public IActionResult Index(int id)
        {
            return View(GetEvents(id));
        }

        // Functions

        public List<Event> GetEvents(int id = 0, string search = "", string status = "", string alphabet = "", string date = "")
        {
            List<Event> events = (from e in _repository.GetEvents() select e).ToList();

            // List<Event> events = (from e in _repository.GetEvents() select e).Reverse().Take(9).ToList();

            // return (from e in _repository.GetEvents() select e).OrderBy(e => e.EventId).Reverse().Take(9).ToList();

            // if (date != null) events = (from e in _repository.GetEvents() select e).Take(9).ToList();

            // if (date == "date2")
            // {
            //     events = (from e in _repository.GetEvents() select e).OrderBy(e => e.EventId).Take(9).ToList();

            //     Console.WriteLine("ABC");
            // }

            if (id > 0)
            {
                events = (from e in _repository.GetEvents() select e).Where(e => e.EventId == id).ToList();
                if (events.Count > 0) Console.WriteLine('✅');
                else Console.WriteLine('❌');
                // Console.WriteLine(events.Count);
            }

            return events;
        }
    }
}
