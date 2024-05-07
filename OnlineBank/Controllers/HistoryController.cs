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
    public class HistoryController : Controller
    {
        public IActionResult Index()
        {
            if (this.Request.Cookies["user-id"] == null)
            {
                return Redirect("/home");
            }

            try
            {
                List<Sending>? sendings = new List<Sending>();

                var userId = int.Parse(this.Request.Cookies["user-id"]);
                HttpClient httpClient = new();
                httpClient.DefaultRequestHeaders.Add("token", Constants.Token);

                HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/users?count={int.MaxValue}").Result;
                _ = response.EnsureSuccessStatusCode();
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                List<User>? users = JsonSerializer.Deserialize<List<User>>(jsonResponse);

                response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/cards?user_id={userId}").Result;
                _ = response.EnsureSuccessStatusCode();
                jsonResponse = response.Content.ReadAsStringAsync().Result;

                List<Card>? userCards = JsonSerializer.Deserialize<List<Card>>(jsonResponse);
                

                //List<Sending> transfers = new();

                foreach (User user in users)
                {
                    response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/cards?user_id={user.UserId}").Result;
                    _ = response.EnsureSuccessStatusCode();
                    jsonResponse = response.Content.ReadAsStringAsync().Result;

                    List<Card> cards = JsonSerializer.Deserialize<List<Card>>(jsonResponse);

                    foreach (Card card in cards)
                    {
                        response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/transfers?card_id={card.SubstanceId}").Result;
                        _ = response.EnsureSuccessStatusCode();
                        jsonResponse = response.Content.ReadAsStringAsync().Result;
                        List<Sending>? newSendings = JsonSerializer.Deserialize<List<Sending>>(jsonResponse);

                        foreach (Sending sending in newSendings)
                        {
                            sending.PersonSurname = user.UserSurname + " " + user.UserName.Substring(0, 1) + ".";
                            sending.AvatarLetter = user.UserSurname.Substring(0, 1);

                            sendings.Add(sending);
                        }

                    }

                }

                sendings.Sort(delegate (Sending x, Sending y)
                {
                    return x.OperationDateTime.CompareTo(y.OperationDateTime);
                });



                sendings.Reverse();

                foreach (Sending sending1 in sendings)
                {
                    foreach (Card userCard in userCards)
                    {
                        if (sending1.SubstanceSenderId == userCard.SubstanceId)
                        {
                            sending1.OperationTypeId = 2;
                        }
                        else if (sending1.SubstanceRecipientId == userCard.SubstanceId)
                        {
                            sending1.OperationTypeId = 3;
                        }
                        
                    }
                }

                //foreach (Sending sending1 in sendings)
                //{
                    //if (sending1.OperationTypeId == 1)
                    //{
                        //sendings.Remove(sending1);
                    //}
                //}

                return View(sendings);
            }
            catch (Exception ex)
            {
                return Redirect("/history");
            }
            
        }
    }
}
