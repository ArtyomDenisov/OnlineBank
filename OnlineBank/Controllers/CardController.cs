using Microsoft.AspNetCore.Mvc;
using OnlineBank.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Web;

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

                httpClient.DefaultRequestHeaders.Add("token", Constants.Token);

                HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/users/{userId}").Result;

                _ = response.EnsureSuccessStatusCode();
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                User user = JsonSerializer.Deserialize<User>(jsonResponse);

                response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/cards?user_id={userId}").Result;

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

                        card.CardNumber = "2202" + card.CardNumber.Substring(4);
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

        [HttpPost]
        public IActionResult AddCard(Card card) // Новая карта
        {
            try
            {
                card.AccountId = Convert.ToInt32(this.Request.Cookies["user-id"]);
                card.Enabled = true;
                card.RublesCount = 1000;

                if(card.ImagePath == "newDesign")
                {
                    if (card.File is null)
                    {
                        TempData["AlertMessageFileNotFound"] = "Файл не выбран!";
                        return View("NewCard");
                    }

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    string fileName = card.File.FileName;
                    string fileType = card.File.ContentType;

                    if (!fileType.Contains("image"))
                    {
                        TempData["AlertMessageNotImage"] = "Неверный формат файла!";
                        return View("NewCard");
                    }

                    string fileNameWithPath = Path.Combine(path, fileName);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        card.File.CopyTo(stream);
                    }

                    card.ImagePath = "Files/" + fileName;
                }

                using StringContent jsonContent = new(
                JsonSerializer.Serialize(card),
                Encoding.UTF8,
                "application/json");

                HttpClient httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("token", Constants.Token);

                HttpResponseMessage response = httpClient.PostAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/cards?user_id={card.AccountId}", jsonContent).Result;

                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception();
                }
                string ? jsonResponse = response.Content.ReadAsStringAsync().Result;
                TempData["AlertMessageCard"] = "Заказ карты прошёл успешно!";

            }
            catch (Exception ex)
            {
                TempData["AlertMessageCardError"] = "Произошла ошибка! Пожалуста, попробуйте повторить операцию позднее!";
                return Redirect("/addcard");
            }

            return Redirect("/home");
        
        
        }
    }
}
