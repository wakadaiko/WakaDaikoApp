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
    public class TaikoTextAdminController : Controller
    {
        private readonly AppDbContext _context;

        public TaikoTextAdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TaikoTextAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaikoText.ToListAsync());
        }

        // GET: TaikoTextAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikoText = await _context.TaikoText
                .FirstOrDefaultAsync(m => m.TextId == id);
            if (taikoText == null)
            {
                return NotFound();
            }

            return View(taikoText);
        }

        // GET: TaikoTextAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaikoTextAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TextId,ConvoId")] TaikoText taikoText)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taikoText);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taikoText);
        }

        // GET: TaikoTextAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikoText = await _context.TaikoText.FindAsync(id);
            if (taikoText == null)
            {
                return NotFound();
            }
            return View(taikoText);
        }

        // POST: TaikoTextAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TextId,ConvoId")] TaikoText taikoText)
        {
            if (id != taikoText.TextId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taikoText);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaikoTextExists(taikoText.TextId))
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
            return View(taikoText);
        }

        // GET: TaikoTextAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikoText = await _context.TaikoText
                .FirstOrDefaultAsync(m => m.TextId == id);
            if (taikoText == null)
            {
                return NotFound();
            }

            return View(taikoText);
        }

        // POST: TaikoTextAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taikoText = await _context.TaikoText.FindAsync(id);
            if (taikoText != null)
            {
                _context.TaikoText.Remove(taikoText);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaikoTextExists(int id)
        {
            return _context.TaikoText.Any(e => e.TextId == id);
        }
    }
}
