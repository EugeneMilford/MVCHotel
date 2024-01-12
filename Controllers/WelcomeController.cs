using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
