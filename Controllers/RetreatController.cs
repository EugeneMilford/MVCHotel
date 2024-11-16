using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Data;
using HotelManagement.Models;

namespace HotelManagement.Controllers
{
    [Authorize] // Ensure all actions require authentication
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Admin"))
            {
                return View(await _context.Spabooking.ToListAsync()); // Admin can see all bookings
            }
            else
            {
                return View(await _context.Spabooking.Where(s => s.UserId == userId).ToListAsync()); // Users can see only their bookings
            }
        }

        // GET: Retreat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Spabooking == null)
            {
                return NotFound();
            }

            var spa = await _context.Spabooking.FirstOrDefaultAsync(m => m.SpaId == id);
            if (spa == null)
            {
                return NotFound();
            }

            // Check if the user is authorized to view this booking
            if (User.IsInRole("User") && spa.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid(); // User trying to access another user's booking
            }

            return View(spa);
        }

        // GET: Retreat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Retreat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpaId,SpaCustomerName,BookingDate,StartTime,EndTime,SpaPackage,Service,IsConfirmed,Price")] Spa spa)
        {
            if (ModelState.IsValid)
            {
                spa.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Associate booking with the current user
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

            // Check if the user is authorized to edit this booking
            if (User.IsInRole("User") && spa.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid(); // User trying to edit another user's booking
            }

            return View(spa);
        }

        // POST: Retreat/Edit/5
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

            var spa = await _context.Spabooking.FirstOrDefaultAsync(m => m.SpaId == id);
            if (spa == null)
            {
                return NotFound();
            }

            // Check if the user is authorized to delete this booking
            if (User.IsInRole("User") && spa.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid(); // User trying to delete another user's booking
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
                return Problem("Entity set 'HotelContext.Spabooking' is null.");
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




