using Microsoft.AspNetCore.Mvc;
using OnlineBank.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace OnlineBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int userId = new();

            if (this.Request?.Cookies["user-id"] != null)
            {
                int.TryParse(this.Request?.Cookies["user-id"]?.ToString(), out userId);
            }

            if (userId <= 0)
            {
                return View(new User());
            }

            HttpClient httpClient = new();

            HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/users/{userId}").Result;

            _ = response.EnsureSuccessStatusCode();

            string jsonResponse = response.Content.ReadAsStringAsync().Result;

            User? user = JsonSerializer.Deserialize<User>(jsonResponse);

            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login() // Вход
        {
            return Redirect("/login");
        }

        public IActionResult Registration() // Регистрация
        {
            return Redirect("/registration");
        }

        public IActionResult Cards() // Карты
        {
            return Redirect("/card");
        }
    }
}
