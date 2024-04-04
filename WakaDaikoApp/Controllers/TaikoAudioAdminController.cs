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
    public class TaikoAudioAdminController : Controller
    {
        private readonly AppDbContext _context;

        public TaikoAudioAdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TaikoAudioAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaikoAudio.ToListAsync());
        }

        // GET: TaikoAudioAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikoAudio = await _context.TaikoAudio
                .FirstOrDefaultAsync(m => m.AudioId == id);
            if (taikoAudio == null)
            {
                return NotFound();
            }

            return View(taikoAudio);
        }

        // GET: TaikoAudioAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaikoAudioAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AudioId,ConvoId")] TaikoAudio taikoAudio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taikoAudio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taikoAudio);
        }

        // GET: TaikoAudioAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikoAudio = await _context.TaikoAudio.FindAsync(id);
            if (taikoAudio == null)
            {
                return NotFound();
            }
            return View(taikoAudio);
        }

        // POST: TaikoAudioAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AudioId,ConvoId")] TaikoAudio taikoAudio)
        {
            if (id != taikoAudio.AudioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taikoAudio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaikoAudioExists(taikoAudio.AudioId))
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
            return View(taikoAudio);
        }

        // GET: TaikoAudioAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikoAudio = await _context.TaikoAudio
                .FirstOrDefaultAsync(m => m.AudioId == id);
            if (taikoAudio == null)
            {
                return NotFound();
            }

            return View(taikoAudio);
        }

        // POST: TaikoAudioAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taikoAudio = await _context.TaikoAudio.FindAsync(id);
            if (taikoAudio != null)
            {
                _context.TaikoAudio.Remove(taikoAudio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaikoAudioExists(int id)
        {
            return _context.TaikoAudio.Any(e => e.AudioId == id);
        }
    }
}
