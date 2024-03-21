using Microsoft.AspNetCore.Mvc;

namespace OnlineBank.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
