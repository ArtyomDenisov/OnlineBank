using Microsoft.AspNetCore.Mvc;
using OnlineBank.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;


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
                using StringContent jsonContent = new(
                JsonSerializer.Serialize(user),
                Encoding.UTF8,
                "application/json");

                HttpResponseMessage response = _httpClient.PostAsync("http://habar-bank-api3.somee.com/api/users/", jsonContent).Result;

                string? jsonResponse = response.Content.ReadAsStringAsync().Result;
                TempData["AlertMessage"] = "Регистрация прошла успешно!";
                if (this.Request.Cookies["user-id"] is null)
                {
                    this.Response.Cookies.Delete("user-id");
                }
                return Redirect("/home");
            }
            return View("Index");
        }
    }
}
