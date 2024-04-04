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
    public class ShimeVideosAdminController : Controller
    {
        private readonly AppDbContext _context;

        public ShimeVideosAdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ShimeVideosAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShimeVideos.ToListAsync());
        }

        // GET: ShimeVideosAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shimeVideos = await _context.ShimeVideos
                .FirstOrDefaultAsync(m => m.VideoId == id);
            if (shimeVideos == null)
            {
                return NotFound();
            }

            return View(shimeVideos);
        }

        // GET: ShimeVideosAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShimeVideosAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VideoId,ConvoId")] ShimeVideos shimeVideos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shimeVideos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shimeVideos);
        }

        // GET: ShimeVideosAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shimeVideos = await _context.ShimeVideos.FindAsync(id);
            if (shimeVideos == null)
            {
                return NotFound();
            }
            return View(shimeVideos);
        }

        // POST: ShimeVideosAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VideoId,ConvoId")] ShimeVideos shimeVideos)
        {
            if (id != shimeVideos.VideoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shimeVideos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShimeVideosExists(shimeVideos.VideoId))
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
            return View(shimeVideos);
        }

        // GET: ShimeVideosAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shimeVideos = await _context.ShimeVideos
                .FirstOrDefaultAsync(m => m.VideoId == id);
            if (shimeVideos == null)
            {
                return NotFound();
            }

            return View(shimeVideos);
        }

        // POST: ShimeVideosAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shimeVideos = await _context.ShimeVideos.FindAsync(id);
            if (shimeVideos != null)
            {
                _context.ShimeVideos.Remove(shimeVideos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShimeVideosExists(int id)
        {
            return _context.ShimeVideos.Any(e => e.VideoId == id);
        }
    }
}
