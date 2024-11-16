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
    public class ContactController : Controller
    {
        private readonly HotelContext _context;

        public ContactController(HotelContext context)
        {
            _context = context;
        }

        // GET: Contact
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Contact
        [HttpPost]
        public async Task<IActionResult> Index(Contact contactMessage)
        {
            if (ModelState.IsValid)
            {
                // Optionally, associate the contact message with the user
                contactMessage.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Associate message with the current user
                _context.ContactMessages.Add(contactMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction("Success"); // Redirect to a success page
            }
            return View(contactMessage);
        }

        // GET: Success
        public IActionResult Success()
        {
            return View();
        }
    }
}




