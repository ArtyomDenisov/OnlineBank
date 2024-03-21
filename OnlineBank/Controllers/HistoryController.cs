using Microsoft.AspNetCore.Mvc;

namespace OnlineBank.Controllers
{
    public class HistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
