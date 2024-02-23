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
    public class CinemasController : Controller
    {
        private readonly HotelContext _context;

        public CinemasController(HotelContext context)
        {
            _context = context;
        }

        // GET: Cinema
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cinema.ToListAsync());
        }

        // GET: Cinema/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cinema == null)
            {
                return NotFound();
            }

            var cinema = await _context.Cinema
                .FirstOrDefaultAsync(m => m.CinemaID == id);
            if (cinema == null)
            {
                return NotFound();
            }

            return View(cinema);
        }

        // GET: Cinema/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cinema/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CinemaID,GuestName,BookingTime,MovieTitle,NumberOfTickets,Confirmed")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cinema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }

        // GET: Cinema/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cinema == null)
            {
                return NotFound();
            }

            var cinema = await _context.Cinema.FindAsync(id);
            if (cinema == null)
            {
                return NotFound();
            }
            return View(cinema);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CinemaID,GuestName,BookingTime,MovieTitle,NumberOfTickets,Confirmed")] Cinema cinema)
        {
            if (id != cinema.CinemaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cinema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemaExists(cinema.CinemaID))
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
            return View(cinema);
        }

        // GET: Cinema/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cinema == null)
            {
                return NotFound();
            }

            var cinema = await _context.Cinema
                .FirstOrDefaultAsync(m => m.CinemaID == id);
            if (cinema == null)
            {
                return NotFound();
            }

            return View(cinema);
        }

        // POST: Cinema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cinema == null)
            {
                return Problem("Entity set 'HotelContext.Cinema'  is null.");
            }

            var cinema = await _context.Cinema.FindAsync(id);
            if (cinema != null)
            {
                _context.Cinema.Remove(cinema);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CinemaExists(int id)
        {
            return _context.Cinema.Any(e => e.CinemaID == id);
        }
    }
}
