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
    public class DrumTeamAdminController : Controller
    {
        private readonly AppDbContext _context;

        public DrumTeamAdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DrumTeamAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.DrumTeam.ToListAsync());
        }

        // GET: DrumTeamAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drumTeam = await _context.DrumTeam
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (drumTeam == null)
            {
                return NotFound();
            }

            return View(drumTeam);
        }

        // GET: DrumTeamAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DrumTeamAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,ConvoId")] DrumTeam drumTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drumTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drumTeam);
        }

        // GET: DrumTeamAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drumTeam = await _context.DrumTeam.FindAsync(id);
            if (drumTeam == null)
            {
                return NotFound();
            }
            return View(drumTeam);
        }

        // POST: DrumTeamAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,ConvoId")] DrumTeam drumTeam)
        {
            if (id != drumTeam.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drumTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrumTeamExists(drumTeam.MemberId))
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
            return View(drumTeam);
        }

        // GET: DrumTeamAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drumTeam = await _context.DrumTeam
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (drumTeam == null)
            {
                return NotFound();
            }

            return View(drumTeam);
        }

        // POST: DrumTeamAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drumTeam = await _context.DrumTeam.FindAsync(id);
            if (drumTeam != null)
            {
                _context.DrumTeam.Remove(drumTeam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrumTeamExists(int id)
        {
            return _context.DrumTeam.Any(e => e.MemberId == id);
        }
    }
}
