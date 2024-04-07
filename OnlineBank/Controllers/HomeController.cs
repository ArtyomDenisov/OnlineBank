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

            ViewBag.ID = user.UserId.ToString();
            ViewBag.Login = user.UserLogin.ToString();
            ViewBag.Name = user.UserName.ToString();
            ViewBag.Surname = user.UserSurname.ToString();
            ViewBag.Patronymic = user.UserPatronymic.ToString();
            ViewBag.Phone = user.UserPhone.ToString();

            response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/cards?user_id={userId}").Result;

            _ = response.EnsureSuccessStatusCode();

            jsonResponse = response.Content.ReadAsStringAsync().Result;

            List<Card>? cards = JsonSerializer.Deserialize<List<Card>>(jsonResponse);

            user.cardList = cards;

            foreach (Card card in cards)
            {
                if (card != null)
                {
                    if (this.Request.Cookies["card-id"] is null)
                    {
                        this.Response.Cookies.Delete("card-id");
                    }
                }
                this.Response.Cookies.Append("card-id", card.SubstanceId.ToString());
            }

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

        public IActionResult GetMessage()
        {
            return PartialView("Cards_Info");
        }
    }
}
