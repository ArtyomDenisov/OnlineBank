using Microsoft.AspNetCore.Mvc;
using OnlineBank.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using System.Net.Http;
using System;

namespace OnlineBank.Controllers
{
    public class CardController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var userId = this.Request.Cookies["user-id"];

                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/users/{userId}").Result;

                _ = response.EnsureSuccessStatusCode();

                string jsonResponse = response.Content.ReadAsStringAsync().Result;

                User user = JsonSerializer.Deserialize<User>(jsonResponse);

                response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/cards?user_id={userId}").Result;

                _ = response.EnsureSuccessStatusCode();

                jsonResponse = response.Content.ReadAsStringAsync().Result;

                List<Card>? cards = JsonSerializer.Deserialize<List<Card>>(jsonResponse);

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
                return View(cards);
            }
            catch(Exception ex)
            {
                return Redirect("/home");
            }          
        }

        public IActionResult NewCard() //NewCard
        {
            return View();
        }
        public IActionResult AddCard(Card card) // Новая карта
        {
            try
            {
                card.AccountId = Convert.ToInt32(this.Request.Cookies["user-id"]);
                //card.CardVariantId = 1;
                //card.ImagePath = "CardDesign2empty.png";
                card.Enabled = true;

                using StringContent jsonContent = new(
                JsonSerializer.Serialize(card),
                Encoding.UTF8,
                "application/json");

                HttpClient httpClient = new HttpClient();

                HttpResponseMessage response = httpClient.PostAsync($"http://habar-bank-api3.somee.com/api/cards?user_id={card.AccountId}", jsonContent).Result;

                string ? jsonResponse = response.Content.ReadAsStringAsync().Result;
                TempData["AlertMessageCard"] = "Заказ карты прошёл успешно!";

            }
            catch (Exception ex)
            {
                TempData["AlertMessageCard"] = "Неверные данные! Пожалуста, повторите вход данных!";
                return Redirect("/addcard");
            }

            return Redirect("/home");
        }
    }
}
