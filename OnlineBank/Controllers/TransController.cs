using Microsoft.AspNetCore.Mvc;

namespace OnlineBank.Controllers
{
    public class TransController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
