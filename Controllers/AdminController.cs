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
        public AdminController(IRepository db, UserManager<AppUser> um, SignInManager<AppUser> sm, RoleManager<IdentityRole> rm) { _db = db;_um = um;_rm = rm;_sm = sm; }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateTeam(string name) { 
            if (ModelState.IsValid)
            {
                var team = new Team{ Name=name,};
                await _db.CreateTeamAsync(team);
                TempData["Message"] = "Success";
                return RedirectToAction("Index");
            }
        return RedirectToAction("Index");
        }
        public async Task<IActionResult> CreateUser()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
