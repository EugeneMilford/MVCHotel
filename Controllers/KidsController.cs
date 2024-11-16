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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Admin"))
            {
                return View(await _context.Kids.ToListAsync()); // Admin can see all bookings
            }
            else
            {
                return View(await _context.Kids.Where(k => k.UserId == userId).ToListAsync()); // Users can see only their bookings
            }
        }

        // GET: PlayArea/Details/5
        public async Task<IActionResult> Details(int? id)
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

            // Check if the user is authorized to view this play area booking
            if (User.IsInRole("User") && playarea.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid(); // User trying to access another user's booking
            }

            return View(playarea);
        }

        // GET: PlayArea/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlayArea/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayAreaID,GuardianName,BookingTime,NumberOfChildren,Confirmed")] PlayArea playarea)
        {
            if (ModelState.IsValid)
            {
                playarea.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Associate booking with the current user
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

            // Check if the user is authorized to edit this booking
            if (User.IsInRole("User") && playarea.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid(); // User trying to edit another user's booking
            }

            return View(playarea);
        }

        // POST: PlayArea/Edit/5
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

            // Check if the user is authorized to delete this booking
            if (User.IsInRole("User") && playarea.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid(); // User trying to delete another user's booking
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
                return Problem("Entity set 'HotelContext.Kids' is null.");
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