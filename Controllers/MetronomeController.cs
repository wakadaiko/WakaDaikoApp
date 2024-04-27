using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WakaDaikoApp.Data;
using WakaDaikoApp.Models;

namespace WakaDaikoApp.Controllers
{
    public class MetronomeController(IRepository r, UserManager<AppUser> um, RoleManager<IdentityRole> rm) : Controller
    {
        // Variables
        readonly private UserManager<AppUser> _userManager = um;
        // readonly private SignInManager<AppUser> _signInManager = sm;
        readonly private RoleManager<IdentityRole> _roleManager = rm;
        readonly private IRepository _repository = r;

        // GET: MetronomeController
        public ActionResult Index()
        {
            return View(new List<int> { 100, 90, 120, 80, 50 });
        }
        // POST: MetronomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Set(IFormCollection collection)
        {
            try
            {
                //message to user success save
                var user = _userManager.GetUserAsync(User).Result;
                user.MetronomePreferances.Add(int.Parse(collection["bpm"]));
                TempData["Message"] = "BPM saved successfully";
                return RedirectToAction("Index", "Metronome", new List<int> { 100, 90, 120, 80, 50 });
            }
            catch
            {
                // tempdata message to user error something went wrong
                TempData["Message"] = "Something went wrong";
                return RedirectToAction("Index", "Metronome", new List<int> { 100, 90, 120, 80, 50 });
            }
        }
        // POST: MetronomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            var list = new List<int> { 100, 90, 120, 80, 50 };
            try
            {
                //Grab current .net users preferred bpm list
                var user = _userManager.GetUserAsync(User).Result;
                user.MetronomePreferances.RemoveAt(id);
                //remove the bpm from the list at index id
                list.RemoveAt(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Message"] = "Something went wrong";
                return RedirectToAction("Index");
            }
        }
    }
}
