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
    public class OutdoorsController : Controller
    {
        private readonly HotelContext _context;

        public OutdoorsController(HotelContext context)
        {
            _context = context;
        }

        // GET: Activity
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Admin"))
            {
                return View(await _context.Activities.ToListAsync()); // Admin can see all bookings
            }
            else
            {
                return View(await _context.Activities.Where(a => a.UserId == userId).ToListAsync()); // Users can see only their bookings
            }
        }

        // GET: Activity/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Activities == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .FirstOrDefaultAsync(m => m.ActivityID == id);
            if (activity == null)
            {
                return NotFound();
            }

            // Check if the user is authorized to view this activity
            if (User.IsInRole("User") && activity.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid(); // User trying to access another user's booking
            }

            return View(activity);
        }

        // GET: Activity/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Activity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityID,GuestName,NumberOfPeople,BookingTime,ActivityName,Confirmed")] Activities activity)
        {
            if (ModelState.IsValid)
            {
                activity.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Associate booking with the current user
                _context.Add(activity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(activity);
        }

        // GET: Activity/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Activities == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            // Check if the user is authorized to edit this activity
            if (User.IsInRole("User") && activity.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid(); // User trying to edit another user's booking
            }

            return View(activity);
        }

        // POST: Activity/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityID,GuestName,NumberOfPeople,BookingTime,ActivityName,Confirmed")] Activities activity)
        {
            if (id != activity.ActivityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(activity.ActivityID))
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
            return View(activity);
        }

        // GET: Activity/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Activities == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .FirstOrDefaultAsync(m => m.ActivityID == id);
            if (activity == null)
            {
                return NotFound();
            }

            // Check if the user is authorized to delete this activity
            if (User.IsInRole("User") && activity.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid(); // User trying to delete another user's booking
            }

            return View(activity);
        }

        // POST: Activity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Activities == null)
            {
                return Problem("Entity set 'HotelContext.Activities' is null.");
            }

            var activity = await _context.Activities.FindAsync(id);
            if (activity != null)
            {
                _context.Activities.Remove(activity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityExists(int id)
        {
            return _context.Activities.Any(e => e.ActivityID == id);
        }
    }
}

