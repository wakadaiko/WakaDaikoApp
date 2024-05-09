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
            if (user != null) { return View(user.MetronomePreferances); 
            }else { return View(new List<int> { }); }           
        }
        // POST: MetronomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Set(int bpmToSave)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null)
            {
                TempData["Message"] = "No-one is logged in.";
                return RedirectToAction("Index", "Metronome", user.MetronomePreferances);
            }
            else
            {
                user.MetronomePreferances.Add(bpmToSave);
                await um.UpdateAsync(user);
                TempData["Message"] = "BPM saved successfully";
                return RedirectToAction(nameof(Index), "Metronome", user.MetronomePreferances);
            }
        }
        // POST: MetronomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int bpmToDelete)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null)
            {
                TempData["Message"] = "Something went wrong";
                return RedirectToAction("Index",user.MetronomePreferances);
            }
            else
            {
               user.MetronomePreferances.RemoveAt(bpmToDelete);
                //remove the bpm from the list at index id
                await um.UpdateAsync(user);
                return RedirectToAction(nameof(Index), "Metronome", user.MetronomePreferances);
            }
        }
    }
}
