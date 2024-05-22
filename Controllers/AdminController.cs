using Microsoft.AspNetCore.Mvc;
using WakaDaikoApp.Data;
using WakaDaikoApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WakaDaikoApp.Controllers
{
    [Authorize(/*Roles = "Site_Admin"*/)]
    public class AdminController(IRepository _r, AppDbContext _c, UserManager<AppUser> _um, RoleManager<IdentityRole> _rm) : Controller
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

        [HttpPost]
        // public async Task<IActionResult> CreateTeam(string name, string teamLead, string description, string Instruments, string members)
        public async Task<IActionResult> CreateTeam(string name, string teamLead, string Instruments, string members)
        {
            if (ModelState.IsValid)
            {
                var _teamLead = await _um.FindByNameAsync(teamLead);
                var team = new Team { Name = name, Description = "The Team of all Teams.", TeamLead = _teamLead };
                var devices = Instruments.ToUpper().Split(',').Select(d => d.Trim()).ToList();
                var teamMembers = members.ToUpper().Split(',').Select(d => d.Trim()).ToList();

                // TeamMembers.ForEach((async m => { team.Members.Add( _um.FindByNameAsync(m).Result); }));

                // Devices.ForEach((async d => { team.Instruments.Add(d); }));

                // foreach (var m in teamMembers) if (await _um.FindByNameAsync(m) != null) team.Members.Add(await _um.FindByNameAsync(m));
                foreach (var m in teamMembers)
                {
                    var memberUser = await _um.FindByNameAsync(m);

                    if (memberUser != null) team.Members?.Add(memberUser);
                }

                // Perhaps add a device table with names and ids
                foreach (var d in devices) team.Instruments?.Add(d);

                var results = await _r.AddTeamAsync(team);

                if (results >= 1)
                {
                    TempData["Message"] = "Success";

                    return RedirectToAction("index");
                }
            }
            else
            {
                TempData["Message"] = "Fail";

                return RedirectToAction("index");
            }

            // We should never get here, but needed to make the environment happy
            return RedirectToAction("index");
        }

        /* string UserName,string UserPassword,string Instruments,string positions, string RollNames,string Teams,string Dependents,string UserEmail="" */

        [HttpPost]
        public async Task<IActionResult> CreateUser(AdminVM form)
        {
            // UserName = $"{UserName.FirstOrDefault().ToString().ToUpper()}{UserName.Substring(1).Select(s=>s.ToString().ToLower())}";

            if (ModelState.IsValid)
            {
                List<string> validRoleNames = [];

                var appUser = new AppUser { Name = form.UserName?.Trim(), UserName = form.UserName?.Trim(), Email = form.UserEmail, };
                var _Dependents = form.Dependents?.ToUpper().Split(',').Select(r => r.Trim()).ToList();
                var _Teams = form.Teams?.ToUpper().Split(',').Select(r => r.Trim()).ToList();
                var _roles = form.RollNames?.ToUpper().Split(',').Select(r => r.Trim()).ToList();
                var _Instruments = form.Instruments?.ToUpper().Split(',').Select(d => d.Trim()).ToList();
                foreach (var r in _roles)
                {
                    if (await _rm.FindByNameAsync(r) != null) { validRoleNames.Add(r); }
                }
                foreach (var d in _Dependents)
                {
                    var dependentUser = await _um.FindByNameAsync(d);
                    if (dependentUser != null) appUser.Family?.Add(dependentUser);
                }
                foreach (var i in _Instruments) { appUser.Instruments?.Add(i); }
                await _um.CreateAsync(appUser, form.UserPassword);
                await _um.AddToRolesAsync(appUser, validRoleNames);
                TempData["Message"] = "Success";
                return RedirectToAction("index");

            }
            else
            {
                TempData["Message"] = "Fail";
                return RedirectToAction("index");
            }
        }
    }
}
