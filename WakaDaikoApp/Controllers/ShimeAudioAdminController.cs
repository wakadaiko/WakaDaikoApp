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
    public class ShimeAudioAdminController : Controller
    {
        private readonly AppDbContext _context;

        public ShimeAudioAdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ShimeAudioAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShimeAudio.ToListAsync());
        }

        // GET: ShimeAudioAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shimeAudio = await _context.ShimeAudio
                .FirstOrDefaultAsync(m => m.AudioId == id);
            if (shimeAudio == null)
            {
                return NotFound();
            }

            return View(shimeAudio);
        }

        // GET: ShimeAudioAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShimeAudioAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AudioId,ConvoId")] ShimeAudio shimeAudio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shimeAudio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shimeAudio);
        }

        // GET: ShimeAudioAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shimeAudio = await _context.ShimeAudio.FindAsync(id);
            if (shimeAudio == null)
            {
                return NotFound();
            }
            return View(shimeAudio);
        }

        // POST: ShimeAudioAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AudioId,ConvoId")] ShimeAudio shimeAudio)
        {
            if (id != shimeAudio.AudioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shimeAudio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShimeAudioExists(shimeAudio.AudioId))
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
            return View(shimeAudio);
        }

        // GET: ShimeAudioAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shimeAudio = await _context.ShimeAudio
                .FirstOrDefaultAsync(m => m.AudioId == id);
            if (shimeAudio == null)
            {
                return NotFound();
            }

            return View(shimeAudio);
        }

        // POST: ShimeAudioAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shimeAudio = await _context.ShimeAudio.FindAsync(id);
            if (shimeAudio != null)
            {
                _context.ShimeAudio.Remove(shimeAudio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShimeAudioExists(int id)
        {
            return _context.ShimeAudio.Any(e => e.AudioId == id);
        }
    }
}
