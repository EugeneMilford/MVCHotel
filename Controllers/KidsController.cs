using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Data;
using HotelManagement.Models;

namespace HotelManagement.Controllers
{
    public class KidsController : Controller
    {
        private readonly HotelContext _context;

        public KidsController(HotelContext context)
        {
            _context = context;
        }

        // GET: PlayArea
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kids.ToListAsync());
        }

        // GET: PlayArea/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kids == null)
            {
                return NotFound();
            }

            var cinema = await _context.Kids
                .FirstOrDefaultAsync(m => m.PlayAreaID == id);
            if (cinema == null)
            {
                return NotFound();
            }

            return View(cinema);
        }

        // GET: PlayArea/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlayArea/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayAreaID,GuardianName,BookingTime,NumberOfChildren,Confirmed")] PlayArea playarea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(playarea);
        }

        // GET: PlayArea/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kids == null)
            {
                return NotFound();
            }

            var playarea = await _context.Kids.FindAsync(id);
            if (playarea == null)
            {
                return NotFound();
            }
            return View(playarea);
        }

        // POST: PlayArea/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayAreaID,GuardianName,BookingTime,NumberOfChildren,Confirmed")] PlayArea playarea)
        {
            if (id != playarea.PlayAreaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayAreaExists(playarea.PlayAreaID))
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
            return View(playarea);
        }

        // GET: PlayArea/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kids == null)
            {
                return NotFound();
            }

            var playarea = await _context.Kids
                .FirstOrDefaultAsync(m => m.PlayAreaID == id);
            if (playarea == null)
            {
                return NotFound();
            }

            return View(playarea);
        }

        // POST: PlayArea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kids == null)
            {
                return Problem("Entity set 'HotelContext.Kids'  is null.");
            }

            var playarea = await _context.Kids.FindAsync(id);
            if (playarea != null)
            {
                _context.Kids.Remove(playarea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayAreaExists(int id)
        {
            return _context.Kids.Any(e => e.PlayAreaID == id);
        }
    }
}
