using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WakaDaikoApp.Data;
using WakaDaikoApp.Models;

namespace WakaDaikoApp.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> _um;
        private SignInManager<AppUser> _sm;
        private RoleManager<IdentityRole> _rm;
        private IRepository _db;
        public AdminController(IRepository db, UserManager<AppUser> um, SignInManager<AppUser> sm, RoleManager<IdentityRole> rm) { _db = db; _um = um; _rm = rm; _sm = sm; }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeam(string name, string teamLead, string description, string instruments, string members)
        {
            if (ModelState.IsValid)
            {
                var teamlead = await _um.FindByNameAsync(teamLead);
                var team = new Team { Name = name, Description = "The Team of all teams.", TeamLead = teamlead };

                var devices = instruments.ToUpper().Split(',').Select(d => d.Trim()).ToList();
                var teamMembers = members.ToUpper().Split(',').Select(d => d.Trim()).ToList();
                //teamMembers.ForEach((async m => { team.Members.Add( _um.FindByNameAsync(m).Result); }));
                //devices.ForEach((async d => { team.Instruments.Add(d); }));
                foreach (var m in teamMembers)
                {
                    if (await _um.FindByNameAsync(m) != null) { team.Members.Add(await _um.FindByNameAsync(m)); }
                }
                // perhaps add a device table with names and id's
                foreach (var d in devices)
                {
                    if (d != null) { team.Instruments.Add(d); }
                }
                var results = await _db.AddTeamAsync(team);
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
            // we should never get here but needed to make the enviorment happy
            return RedirectToAction("Index");
        }
        /*  string userName,string userPassword,string instruments,string positions, string rollNames,string teams,string dependants,string email="" */
        [HttpPost]
        public async Task<IActionResult> CreateUser(AdminVM form)
        {
            //userName = $"{userName.FirstOrDefault().ToString().ToUpper()}{userName.Substring(1).Select(s=>s.ToString().ToLower())}";

            if (ModelState.IsValid)
            {
                List<String> validRoleNames = new List<String>();
                var appUser = new AppUser { UserName = form.userName.Trim(), Email = form.email, };
                var _dependants = form.dependants.ToUpper().Split(',').Select(r => r.Trim()).ToList();
                var _teams = form.teams.ToUpper().Split(',').Select(r => r.Trim()).ToList();
                var _roles = form.rollNames.ToUpper().Split(',').Select(r => r.Trim()).ToList();
                var teamOBJs = await _db.GetTeamsByNameAsync(_teams);
                var _instruments = form.instruments.ToUpper().Split(',').Select(d => d.Trim()).ToList();
                // the lambda should work here but it has its drawbacks
                //_roles.ForEach(async r => { if ( await _rm.FindByNameAsync(r) != null) { validRoleNames.Add(r); } });
                foreach (var r in _roles)
                {
                    if (await _rm.FindByNameAsync(r) != null) { validRoleNames.Add(r); }
                }
                /*foreach (var t in teamOBJs){
                    if (t != null) { appUser.Teams.Add(t); }
                }*/
                foreach (var d in _dependants)
                {
                    if (await _um.FindByNameAsync(d) != null) { appUser.Family.Add(await _um.FindByNameAsync(d)); }
                }
                foreach (var i in _instruments)
                {
                    appUser.Instruments.Add(i);
                }
                await _um.CreateAsync(appUser, form.userPassword);
                await _um.AddToRolesAsync(appUser, validRoleNames);
                TempData["Message"] = "Success";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Fail";
            return RedirectToAction("Index");
        }
    }
}
