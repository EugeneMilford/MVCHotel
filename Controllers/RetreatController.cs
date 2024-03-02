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
    public class RetreatController : Controller
    {
        private readonly HotelContext _context;

        public RetreatController(HotelContext context)
        {
            _context = context;
        }

        // GET: Retreat
        public async Task<IActionResult> Index()
        {
            return View(await _context.Spabooking.ToListAsync());
        }

        // GET: Retreat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Spabooking == null)
            {
                return NotFound();
            }

            var spa = await _context.Spabooking
                .FirstOrDefaultAsync(m => m.SpaId == id);
            if (spa == null)
            {
                return NotFound();
            }

            return View(spa);
        }

        // GET: Retreat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Retreat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpaId,SpaCustomerName,BookingDate,StartTime,EndTime,SpaPackage,Service,IsConfirmed,Price")] Spa spa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spa);
        }

        // GET: Retreat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Spabooking == null)
            {
                return NotFound();
            }

            var spa = await _context.Spabooking.FindAsync(id);
            if (spa == null)
            {
                return NotFound();
            }
            return View(spa);
        }

        // POST: Retreat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpaId,SpaCustomerName,BookingDate,StartTime,EndTime,SpaPackage,Service,IsConfirmed,Price")] Spa spa)
        {
            if (id != spa.SpaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpaExists(spa.SpaId))
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
            return View(spa);
        }

        // GET: Retreat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Spabooking == null)
            {
                return NotFound();
            }

            var spa = await _context.Spabooking
                .FirstOrDefaultAsync(m => m.SpaId == id);
            if (spa == null)
            {
                return NotFound();
            }

            return View(spa);
        }

        // POST: Retreat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Spabooking == null)
            {
                return Problem("Entity set 'HotelContext.SpaBooking  is null.");
            }

            var spa = await _context.Spabooking.FindAsync(id);
            if (spa != null)
            {
                _context.Spabooking.Remove(spa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpaExists(int id)
        {
            return _context.Spabooking.Any(e => e.SpaId == id);
        }
    }
}






