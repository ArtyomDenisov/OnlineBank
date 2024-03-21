using Microsoft.AspNetCore.Mvc;
using OnlineBank.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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


                ViewBag.Name = user.UserName.ToString();
                ViewBag.Surname = user.UserSurname.ToString();

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

        public IActionResult AddCard() // Новая карта
        {
            return View("NewCard");
        }
    }
}
