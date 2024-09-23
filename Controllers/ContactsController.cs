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
    //public class ContactsController : Controller
    //{
    //    private readonly HotelContext _context;

    //    public ContactsController(HotelContext context) 
    //    {
    //        _context = context;
    //    }

    //    // GET: Contact
    //    public async Task<IActionResult> Index()
    //    {
    //        return View(await _context.Cinema.ToListAsync());
    //    }
    //}

    namespace HotelManagement.Controllers
    {
        public class ContactController : Controller
        {
            private readonly HotelContext _context;

            public ContactController(HotelContext context)
            {
                _context = context;
            }

            [HttpGet]
            public IActionResult Index()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Index(Contact contactMessage)
            {
                if (ModelState.IsValid)
                {
                    _context.ContactMessages.Add(contactMessage);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Success"); // Redirect to a success page
                }
                return View(contactMessage);
            }

            public IActionResult Success()
            {
                return View();
            }
        }
    }

}




