using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
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
        public AdminController(IRepository db, UserManager<AppUser> um, SignInManager<AppUser> sm, RoleManager<IdentityRole> rm) { _db = db;_um = um;_rm = rm;_sm = sm; }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeam(string name,string teamLead,string description,string instruments,string members) { 
            if (ModelState.IsValid)
            {
                var teamlead = await _um.FindByNameAsync("admin");
                var team = new Team{ Name=name,Description="The Team of all teams.",TeamLead= teamlead};

                var devices = instruments.ToUpper().Split(',').Select(d => d.Trim()).ToList();
                var teamMembers = members.ToUpper().Split(',').Select(d => d.Trim()).ToList();
                //teamMembers.ForEach((async m => { team.Members.Add( _um.FindByNameAsync(m).Result); }));
                //devices.ForEach((async d => { team.Instruments.Add(d); }));
                foreach (var m in teamMembers) { team.Members.Add(await _um.FindByNameAsync(m)); }
                foreach (var d in devices) { team.Instruments.Add(d); }
                var results = await _db.AddTeamAsync(team);
                if (results > 1) {
                    TempData["Message"] = "Success";
                    return RedirectToAction("Index");
                }
                
            }
            TempData["Message"] = "Fail";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(string userName,string userPassword,string instruments,string positions, string rollNames)
        {
//userName = $"{userName.FirstOrDefault().ToString().ToUpper()}{userName.Substring(1).Select(s=>s.ToString().ToLower())}";
            if (ModelState.IsValid)
            {
                var roles = rollNames.ToUpper().Split(',').Select(r=>r.Trim());
                var devices = instruments.ToUpper().Split(',').Select(d=>d.Trim());
                var appUser = new AppUser { UserName=userName.Trim(),};
                await _um.CreateAsync(appUser, userPassword);
                await _um.AddToRolesAsync(appUser,roles);
                TempData["Message"] = "Success";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Fail";
            return RedirectToAction("Index");
        }
    }
}
