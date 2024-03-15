using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WakaDaikoApp.Data;
using WakaDaikoApp.Models;

namespace WakaDaikoApp.Controllers
{
    public class ShimeTextAdminController : Controller
    {
        private readonly AppDbContext _context;

        public ShimeTextAdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ShimeTextAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShimeText.ToListAsync());
        }

        // GET: ShimeTextAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shimeText = await _context.ShimeText
                .FirstOrDefaultAsync(m => m.TextId == id);
            if (shimeText == null)
            {
                return NotFound();
            }

            return View(shimeText);
        }

        // GET: ShimeTextAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShimeTextAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TextId,ConvoId")] ShimeText shimeText)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shimeText);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shimeText);
        }

        // GET: ShimeTextAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shimeText = await _context.ShimeText.FindAsync(id);
            if (shimeText == null)
            {
                return NotFound();
            }
            return View(shimeText);
        }

        // POST: ShimeTextAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TextId,ConvoId")] ShimeText shimeText)
        {
            if (id != shimeText.TextId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shimeText);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShimeTextExists(shimeText.TextId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shimeText);
        }

        // GET: ShimeTextAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shimeText = await _context.ShimeText
                .FirstOrDefaultAsync(m => m.TextId == id);
            if (shimeText == null)
            {
                return NotFound();
            }

            return View(shimeText);
        }

        // POST: ShimeTextAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shimeText = await _context.ShimeText.FindAsync(id);
            if (shimeText != null)
            {
                _context.ShimeText.Remove(shimeText);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShimeTextExists(int id)
        {
            return _context.ShimeText.Any(e => e.TextId == id);
        }
    }
}
