using Microsoft.AspNetCore.Mvc;
using OnlineBank.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace OnlineBank.Controllers
{
    public class RegistrationController : Controller
    {
        private static readonly HttpClient _httpClient = new();

        public IActionResult Index() // Registration
        {
            return View();
        }

        [HttpPost]
        public IActionResult Check(User user) // Регистрация пользователя
        {
       
            if (ModelState.IsValid)
            {
                try
                {
                    user.UserEnabled = true;
                    user.UserUserLevelId = 1;
                    using StringContent jsonContent = new(
                    JsonSerializer.Serialize(user),
                    Encoding.UTF8,
                    "application/json");

                    _httpClient.DefaultRequestHeaders.Add("token", Constants.Token);

                    HttpResponseMessage response = _httpClient.PostAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/users/", jsonContent).Result;
                    string? jsonResponse = response.Content.ReadAsStringAsync().Result;
                    TempData["RegistrationMessage"] = "Регистрация прошла успешно!";
                    
                    if (this.Request.Cookies["user-id"] is null)
                    {
                        this.Response.Cookies.Delete("user-id");
                    }
                    return Redirect("/home");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    TempData["AlertMessage"] = "Некоректные данные! Пожалуста, повторите вход снова!";
                    return View("Index");
                }

            }
            return View("Index");
        }
    }
}
