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
            var user = _userManager.GetUserAsync(User).Result;
            // pass the logged in user to the view
            return View(new List<int> { 90, 120, 80, 50 });
        }
        // POST: MetronomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Set(int bpmToSave)
        {
            new List<int> { 100, 90, 120, 80, 50 };
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null)
            {
                TempData["Message"] = "Something went wrong";
                return RedirectToAction("Index", "Metronome", new List<int> { 100, 90, 120, 80, 50 });
            }
            else
            {
                user.MetronomePreferances.Add(bpmToSave);
                TempData["Message"] = "BPM saved successfully";
                return RedirectToAction(nameof(Index), "Metronome", user.MetronomePreferances);
            }
        }
        // POST: MetronomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int bpmToDelete)
        {
            var list = new List<int> { 100, 90, 120, 80, 50 };
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null)
            {
                TempData["Message"] = "Something went wrong";
                return RedirectToAction("Index");
            }
            else
            {
                user.MetronomePreferances.RemoveAt(bpmToDelete);
                //remove the bpm from the list at index id
                list.RemoveAt(bpmToDelete);
                return RedirectToAction(nameof(Index), "Metronome", user.MetronomePreferances);
            }
        }
    }
}
