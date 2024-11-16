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
    public class RestaurantsController : Controller
    {
        private readonly HotelContext _context;

        public RestaurantsController(HotelContext context)
        {
            _context = context;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Admin"))
            {
                return View(await _context.Restaurant.ToListAsync()); // Admin can see all bookings
            }
            else
            {
                return View(await _context.Restaurant.Where(r => r.UserId == userId).ToListAsync()); // Users can see only their bookings
            }
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Restaurant == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant
                .FirstOrDefaultAsync(m => m.RestaurantID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            // Check if the user is authorized to view this booking
            if (User.IsInRole("User") && restaurant.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid(); // User trying to access another user's booking
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestaurantID,CustomerName,BookingTime,NumberOfGuests,Confirmed")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                restaurant.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Associate booking with the current user
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Restaurant == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            // Check if the user is authorized to edit this booking
            if (User.IsInRole("User") && restaurant.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid(); // User trying to edit another user's booking
            }

            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RestaurantID,CustomerName,BookingTime,NumberOfGuests,Confirmed")] Restaurant restaurant)
        {
            if (id != restaurant.RestaurantID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.RestaurantID))
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
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Restaurant == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant
                .FirstOrDefaultAsync(m => m.RestaurantID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            // Check if the user is authorized to delete this booking
            if (User.IsInRole("User") && restaurant.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid(); // User trying to delete another user's booking
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Restaurant == null)
            {
                return Problem("Entity set 'HotelContext.Restaurant'  is null.");
            }

            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurant.Remove(restaurant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurant.Any(e => e.RestaurantID == id);
        }
    }
}

