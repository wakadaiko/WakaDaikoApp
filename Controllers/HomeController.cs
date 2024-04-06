using Microsoft.AspNetCore.Mvc;
using WakaDaikoApp.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WakaDaikoApp.Data;

namespace WakaDaikoApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }
        public IActionResult Index() => View();

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
