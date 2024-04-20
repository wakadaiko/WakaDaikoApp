using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WakaDaikoApp.Controllers.ControllersKG
{
    public class MetronomeController : Controller
    {
        // GET: MetronomeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MetronomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MetronomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MetronomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MetronomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MetronomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MetronomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MetronomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
