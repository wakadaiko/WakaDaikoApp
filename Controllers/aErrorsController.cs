using Microsoft.AspNetCore.Mvc;
using WakaDaikoApp.Models;
using System.Diagnostics;

namespace WakaDaikoApp.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("~/views/shared/errors/{statuscode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "ERRRRRRRRROR";
                    break;
            }

            return View("NotFound");
        }
    }
}
