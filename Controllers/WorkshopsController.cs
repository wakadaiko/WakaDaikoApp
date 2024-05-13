using Microsoft.AspNetCore.Mvc;
using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
using System.Diagnostics;

namespace WakaDaikoApp.Controllers
{
    public class WorkshopsController(IRepository _r, AppDbContext _c) : Controller
    {
        // Functions

        public async Task GetPinnedBanner()
        {
            List<Event> events = [
                .. _r
                .GetEvents()
                .Where(e => e.Pinned == true)
            ];

            Event _event;

            switch (events.Count)
            {
                case 1:
                    _event = await _r.GetEventByIdAsync(events[0].EventId);

                    ViewBag.BannerTitle = _event.Title;
                    ViewBag.BannerDate = _event.Date;
                    ViewBag.Description = _event.Description;

                    break;
                case int n when n > 1:
                    foreach (var e in events)
                    {
                        e.Pinned = false;

                        _c.Update(e);

                    }

                    await _c.SaveChangesAsync();

                    Console.WriteLine("More than one pinned event was found. Reverting all pinned Events.");

                    break;
            }
        }

        // Controllers & Routes

        public async Task<IActionResult> Index()
        {
            await GetPinnedBanner();

            return View();
        }
    }
}
