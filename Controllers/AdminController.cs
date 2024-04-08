using WakaDaikoApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
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
            return RedirectToAction("Index", "Home");
        }
    }
}
