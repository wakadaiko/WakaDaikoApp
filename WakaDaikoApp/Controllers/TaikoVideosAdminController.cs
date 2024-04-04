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
    public class TaikoVideosAdminController : Controller
    {
        private readonly AppDbContext _context;

        public TaikoVideosAdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TaikoVideosAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaikoVideos.ToListAsync());
        }

        // GET: TaikoVideosAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikoVideos = await _context.TaikoVideos
                .FirstOrDefaultAsync(m => m.VideoId == id);
            if (taikoVideos == null)
            {
                return NotFound();
            }

            return View(taikoVideos);
        }

        // GET: TaikoVideosAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaikoVideosAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VideoId,ConvoId")] TaikoVideos taikoVideos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taikoVideos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taikoVideos);
        }

        // GET: TaikoVideosAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikoVideos = await _context.TaikoVideos.FindAsync(id);
            if (taikoVideos == null)
            {
                return NotFound();
            }
            return View(taikoVideos);
        }

        // POST: TaikoVideosAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VideoId,ConvoId")] TaikoVideos taikoVideos)
        {
            if (id != taikoVideos.VideoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taikoVideos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaikoVideosExists(taikoVideos.VideoId))
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
            return View(taikoVideos);
        }

        // GET: TaikoVideosAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikoVideos = await _context.TaikoVideos
                .FirstOrDefaultAsync(m => m.VideoId == id);
            if (taikoVideos == null)
            {
                return NotFound();
            }

            return View(taikoVideos);
        }

        // POST: TaikoVideosAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taikoVideos = await _context.TaikoVideos.FindAsync(id);
            if (taikoVideos != null)
            {
                _context.TaikoVideos.Remove(taikoVideos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaikoVideosExists(int id)
        {
            return _context.TaikoVideos.Any(e => e.VideoId == id);
        }
    }
}
