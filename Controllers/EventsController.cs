using Microsoft.AspNetCore.Mvc;
using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace WakaDaikoApp.Controllers
{
    public class EventsController(IRepository _r, AppDbContext _c) : Controller
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

                    break;
                case int n when n > 1:
                    throw new Exception("More than one pinned event was found.");
            }
        }

        // Controllers & Routes

        public async Task<IActionResult> Index()
        {
            await GetPinnedBanner();

            string? search = Request.Query["search"];
            string? status = Request.Query["status"];
            string? alphabet = Request.Query["alphabet"];
            string? date = Request.Query["date"];

            return View("Index", GetEvents("", "", search ?? "", status ?? "", alphabet ?? "", date ?? ""));
        }

        [HttpGet("/events/page/{paginationId}")]
        public async Task<IActionResult> Index(string paginationId)
        {
            await GetPinnedBanner();

            string? search = Request.Query["search"];
            string? status = Request.Query["status"];
            string? alphabet = Request.Query["alphabet"];
            string? date = Request.Query["date"];

            return View(GetEvents(paginationId ?? "", "", search ?? "", status ?? "", alphabet ?? "", date ?? ""));
        }

        // Workaround - Extra [used] parameter

        [HttpGet("/events/view/{descriptionText}")]
        public async Task<IActionResult> Index(string descriptionText, string _)
        {
            await GetPinnedBanner();

            return View(GetEvents("", descriptionText ?? ""));
        }

        // Workaround - Extra [used] parameter
        [Authorize(Roles = "Admin")]

        [HttpGet("/events/pin/{pinId}")]
        public async Task<IActionResult> Index(string pinId, string _, string __)
        {
            await GetPinnedBanner();

            try
            {
                bool b = int.TryParse(pinId, out int pinId2);

                Event? _event = null;

                if (await _r.GetEventByIdAsync(pinId2) != null) _event = await _r.GetEventByIdAsync(pinId2);

                if (_event != null)
                {
                    _c.Update(_event);

                    List<Event> events = [
                        .. _r
                    .GetEvents()
                    ];

                    foreach (var e in events)
                    {
                        e.Pinned = false;

                        _c.Update(e);

                        await _c.SaveChangesAsync();
                    }

                    _event.Pinned = true;

                    await _c.SaveChangesAsync();

                    return Redirect("/events");
                }
            }
            catch
            {
                return NotFound();
            }

            return NotFound();
        }

        // Functions

        public List<Event> GetEvents(string paginationId = "", string descriptionText = "", string search = "", string status = "", string alphabet = "", string date = "")
        {
            // Variables

            int EVENTS_PER_PAGE = 3;

            _ = int.TryParse(paginationId, out int paginationId2);

            // Load initial events

            List<Event> events = [
                .. _r
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

            if (descriptionText.Length > 0) events = [.. events.Where(e => e.Title != null && e.Description == descriptionText)];

            // Search

            if (search != "") events = [.. events.Where(e => e.Title != null && e.Title.Contains(search, StringComparison.CurrentCultureIgnoreCase) || (e.Text != null && e.Text.Contains(search, StringComparison.CurrentCultureIgnoreCase)))];

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
