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
    public class TransController : Controller
    {
        public IActionResult Index()
        {
            if (this.Request.Cookies["user-id"] == null)
            {
                return Redirect("/home");
            }
            return View();
        }

        public IActionResult Check(Sending sending)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = this.Request.Cookies["user-id"];
                    bool isCardExist = false;
                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Add("token", Constants.Token);
                    HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/cards?user_id={userId}").Result;
                    _ = response.EnsureSuccessStatusCode();
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;

                    List<Card> userCards = JsonSerializer.Deserialize<List<Card>>(jsonResponse);

                    foreach (Card userCard in userCards)
                    {
                        if (userCard.SubstanceId == sending.SubstanceId)
                        {
                            isCardExist = true;
                            if (userCard.RublesCount < sending.RublesCount)
                            {
                                TempData["AlertMessageSendingRublesCount"] = "На карте недостаточно средств!";
                                return View("Index");
                            }
                            break;
                        }
                    }
                    if (!isCardExist)
                    {
                        TempData["AlertMessageSendingCardExist"] = "Неверный номер карты!";
                        return View("Index");
                    }

                    sending.OperationDateTime = DateTime.Now;
                    sending.SubstanceSenderId = sending.SubstanceId;
                    sending.OperationTypeId = 1;
                    sending.Enabled = true;

                    httpClient = new HttpClient();

                    httpClient.DefaultRequestHeaders.Add("token", Constants.Token);
                    response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/users?count={int.MaxValue}").Result;
                    _ = response.EnsureSuccessStatusCode();

                    jsonResponse = response.Content.ReadAsStringAsync().Result;
                    List<User> users = JsonSerializer.Deserialize<List<User>>(jsonResponse);

                    List<Card> cards = new List<Card>();
                    foreach (User user in users)
                    {
                        response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/cards?user_id={user.UserId}").Result;
                        _ = response.EnsureSuccessStatusCode();
                        jsonResponse = response.Content.ReadAsStringAsync().Result;
                        List<Card>? newCards = JsonSerializer.Deserialize<List<Card>>(jsonResponse);
                        foreach (Card newCard in newCards)
                        {
                            cards.Add(newCard);
                        }
                        
                    }

                    sending.CardNumber = sending.CardNumber.Remove(0, 4);
                    Card card = cards.Find(x => x.CardNumber.Remove(0, 4).Contains(sending.CardNumber));
                    
                    if (card is null)
                    {
                        TempData["AlertMessageSendingUser"] = "Карта для перевода не найдена!";
                        return View("Index");
                    }

                    sending.SubstanceRecipientId = card.SubstanceId;

                    using StringContent jsonContent = new(
                    JsonSerializer.Serialize(sending),
                    Encoding.UTF8,
                    "application/json");

                    response = httpClient.PostAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/transfers", jsonContent).Result;

                    jsonResponse = response.Content.ReadAsStringAsync().Result;
                    TempData["AlertMessageSending"] = "Операция прошла успешно!";

                }
                catch (Exception ex)
                {
                    TempData["AlertMessageSendingError"] = "Произошда ошибка! Пожалуста, повторите вход данных!";
                    return Redirect("/trans");
                }
                return Redirect("/trans");
            }
            return View("Index");
                
        }
    }


}
