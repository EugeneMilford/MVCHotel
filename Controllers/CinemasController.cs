using HotelManagement.Areas.Identity.Data;
using HotelManagement.Data;
using HotelManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelManagement.Controllers
{
    [Authorize] // Ensure that the user is authenticated
    public class CinemasController : Controller
    {
        private readonly HotelContext _context;
        private readonly UserManager<HotelUser> _userManager;

        public CinemasController(HotelContext context, UserManager<HotelUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Cinema
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID
            var cinemaBookings = await _context.Cinema
                                               .Where(c => c.UserId == userId) // Filter by user
                                               .ToListAsync();
            return View(cinemaBookings);
        }

        // GET: Cinema/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cinema/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuestName,BookingTime,MovieTitle,NumberOfTickets")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                cinema.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Assign current user's ID
                _context.Add(cinema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }

        // GET: Cinema/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var cinema = await _context.Cinema.FindAsync(id);
            if (cinema == null || cinema.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound(); // Prevent access if the user does not own the booking
            }

            return View(cinema);
        }

        // POST: Cinema/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CinemaID,GuestName,BookingTime,MovieTitle,NumberOfTickets,Confirmed")] Cinema cinema)
        {
            if (id != cinema.CinemaID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    cinema.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Ensure the correct user is editing
                    _context.Update(cinema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemaExists(cinema.CinemaID))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }

        // GET: Cinema/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var cinema = await _context.Cinema
                .FirstOrDefaultAsync(m => m.CinemaID == id);
            if (cinema == null || cinema.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound(); // Prevent access if the user does not own the booking
            }

            return View(cinema);
        }

        // POST: Cinema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinema = await _context.Cinema.FindAsync(id);
            if (cinema != null && cinema.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                _context.Cinema.Remove(cinema);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CinemaExists(int id)
        {
            return _context.Cinema.Any(e => e.CinemaID == id);
        }
    }
}