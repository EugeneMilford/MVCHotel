using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
