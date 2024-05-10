using Microsoft.AspNetCore.Mvc;
using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
using System.Diagnostics;

namespace WakaDaikoApp.Controllers
{
    public class HomeController(IRepository _r, AppDbContext _c) : Controller
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

        public async Task<IActionResult> Privacy()
        {
            await GetPinnedBanner();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        [Route("/Home/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            ViewData["ErrorMessage"] = $"Error occurred. The ErrorCode is: {code}";

            return View("~/Views/Shared/404.cshtml");
        }
    }
}
