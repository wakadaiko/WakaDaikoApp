using WakaDaikoApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WakaDaikoApp.Models;

namespace WakaDaikoApp.Controllers
{
    public class AccountController(IRepository _r, AppDbContext _c, UserManager<AppUser> _um, SignInManager<AppUser> _sm) : Controller
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

        public async Task<IActionResult> Register()
        {
            await GetPinnedBanner();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            await GetPinnedBanner();

            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Username, Name = model.Name };
                var result = await _um.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _sm.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

                    return RedirectToAction("index", "home");
                }
                else foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            }

            ViewBag.Name = model.Name;
            ViewBag.Username = model.Username;

            ModelState.AddModelError("", "Invalid name, username, password or confirm password");

            return View(model);
        }

        public async Task<IActionResult> Login(string returnURL = "")
        {
            // await GetPinnedBanner();

            var model = new LoginVM { ReturnUrl = returnURL };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            // await GetPinnedBanner();

            if (ModelState.IsValid)
            {
                var result = await _sm.PasswordSignInAsync(model.Username, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded) return (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)) ? Redirect(model.ReturnUrl) : RedirectToAction("Index", "home");
            }

            ViewBag.Username = model.Username;

            ModelState.AddModelError("", "Invalid username or password");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // await GetPinnedBanner();

            await _sm.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> AccessDenied()
        {
            await GetPinnedBanner();

            return View();
        }
    }
}
