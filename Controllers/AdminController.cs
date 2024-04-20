using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WakaDaikoApp.Data;
using WakaDaikoApp.Models;

namespace WakaDaikoApp.Controllers
{
    // public class AdminController(IRepository r, UserManager<AppUser> um, SignInManager<AppUser> sm, RoleManager<IdentityRole> rm) : Controller
    public class AdminController(IRepository r, UserManager<AppUser> um, RoleManager<IdentityRole> rm) : Controller
    {
        // Variables

        readonly private UserManager<AppUser> _userManager = um;

        // readonly private SignInManager<AppUser> _signInManager = sm;

        readonly private RoleManager<IdentityRole> _roleManager = rm;

        readonly private IRepository _repository = r;

        // Functions

        public async Task GetPinnedBanner()
        {
            List<Event> events = [
                .. _repository
                .GetEvents()
                .Where(e => e.Pinned == true)
            ];

            Event _event;

            switch (events.Count)
            {
                case 1:
                    _event = await _repository.GetEventByIdAsync(events[0].EventId);

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

            return View();
        }

        [HttpPost]
        // public async Task<IActionResult> CreateTeam(string name, string teamLead, string description, string Instruments, string members)
        public async Task<IActionResult> CreateTeam(string name, string teamLead, string Instruments, string members)
        {
            if (ModelState.IsValid)
            {
                var _teamLead = await _userManager.FindByNameAsync(teamLead);
                var team = new Team { Name = name, Description = "The Team of all Teams.", TeamLead = _teamLead };
                var devices = Instruments.ToUpper().Split(',').Select(d => d.Trim()).ToList();
                var teamMembers = members.ToUpper().Split(',').Select(d => d.Trim()).ToList();

                // TeamMembers.ForEach((async m => { team.Members.Add( _userManager.FindByNameAsync(m).Result); }));

                // Devices.ForEach((async d => { team.Instruments.Add(d); }));

                // foreach (var m in teamMembers) if (await _userManager.FindByNameAsync(m) != null) team.Members.Add(await _userManager.FindByNameAsync(m));
                foreach (var m in teamMembers)
                {
                    var memberUser = await _userManager.FindByNameAsync(m);

                    if (memberUser != null) team.Members?.Add(memberUser);
                }

                // Perhaps add a device table with names and ids
                foreach (var d in devices) team.Instruments?.Add(d);

                var results = await _repository.AddTeamAsync(team);

                if (results >= 1)
                {
                    TempData["Message"] = "Success";

                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Message"] = "Fail";

                return RedirectToAction("Index");
            }

            // We should never get here, but needed to make the environment happy
            return RedirectToAction("Index");
        }

        /* string UserName,string UserPassword,string Instruments,string positions, string RollNames,string Teams,string Dependents,string UserEmail="" */

        [HttpPost]
        public async Task<IActionResult> CreateUser(AdminVM form)
        {
            // UserName = $"{UserName.FirstOrDefault().ToString().ToUpper()}{UserName.Substring(1).Select(s=>s.ToString().ToLower())}";

            if (ModelState.IsValid)
            {
                List<string> validRoleNames = [];

                var appUser = new AppUser { UserName = form.UserName?.Trim(), Email = form.UserEmail, };
                var _Dependents = form.Dependents?.ToUpper().Split(',').Select(r => r.Trim()).ToList();
                var _Teams = form.Teams?.ToUpper().Split(',').Select(r => r.Trim()).ToList();
                var _roles = form.RollNames?.ToUpper().Split(',').Select(r => r.Trim()).ToList();

                // var teamOBJs = "";
                // if (_Teams != null) await _repository.GetTeamsByNameAsync(_Teams);

                var _Instruments = form.Instruments?.ToUpper().Split(',').Select(d => d.Trim()).ToList();

                // The lambda should work here, but it has its drawbacks
                //_roles.ForEach(async r => { if ( await _roleManager.FindByNameAsync(r) != null) { validRoleNames.Add(r); } });

                if (_roles != null) foreach (var r in _roles) if (await _roleManager.FindByNameAsync(r) != null) { validRoleNames.Add(r); }

                /* foreach (var t in teamOBJs) if (t != null) appUser.Teams.Add(t); */

                if (_Dependents != null)
                {
                    foreach (var d in _Dependents)
                    {
                        var dependentUser = await _userManager.FindByNameAsync(d);

                        if (dependentUser != null) appUser.Family?.Add(dependentUser);
                    }
                }
                if (_Instruments != null) foreach (var i in _Instruments) appUser.Instruments?.Add(i);
                if (form.UserPassword != null) await _userManager.CreateAsync(appUser, form.UserPassword);

                await _userManager.AddToRolesAsync(appUser, validRoleNames);

                TempData["Message"] = "Success";

                return RedirectToAction("Index");
            }

            TempData["Message"] = "Fail";

            return RedirectToAction("Index");
        }
    }
}
