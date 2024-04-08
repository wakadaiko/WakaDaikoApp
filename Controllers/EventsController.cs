using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WakaDaikoApp.Controllers
{
    public class EventsController(IRepository r, UserManager<AppUser> u) : Controller
    {
        readonly IRepository _repository = r;

        readonly UserManager<AppUser> _userManager = u;

        public IActionResult Index()
        {
            List<Event> events = (from e in _repository.GetEvents() select e).Take(9).ToList();

            return View("Index", events);
        }
    }
}
