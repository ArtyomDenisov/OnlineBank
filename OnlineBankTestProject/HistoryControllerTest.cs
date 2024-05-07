using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineBank;
using OnlineBank.Controllers;
using OnlineBank.Models;
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBankTestProject
{
    [TestClass]
    public class HistoryControllerTest
    {
        [TestMethod]
        public void SuccessHistory()
        {
            List<Sending> sendings = new List<Sending>();
            int userId = 8;

            HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("token", Constants.Token);

            HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/users?count={int.MaxValue}").Result;
            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            List<User> users = JsonSerializer.Deserialize<List<User>>(jsonResponse);

            response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/cards?user_id={userId}").Result;
            jsonResponse = response.Content.ReadAsStringAsync().Result;

            List<Card> userCards = JsonSerializer.Deserialize<List<Card>>(jsonResponse);

            foreach (User user in users)
            {
                response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/cards?user_id={user.UserId}").Result;
                jsonResponse = response.Content.ReadAsStringAsync().Result;

                List<Card> cards = JsonSerializer.Deserialize<List<Card>>(jsonResponse);

                foreach (Card card in cards)
                {
                    response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/transfers?card_id={card.SubstanceId}").Result;
                    jsonResponse = response.Content.ReadAsStringAsync().Result;
                    List<Sending> newSendings = JsonSerializer.Deserialize<List<Sending>>(jsonResponse);

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

            List<Sending> item = new List<Sending>();
            item.Add(new Sending("Д",true,new DateTime(2024,4,22,4,41,38),1,"Денисов А.", 100, 4, 14, 2, 14));
            item.Add(new Sending("С",true,new DateTime(2024,4,23,17,15,12),1,"Сухих М.", 200, 6, 20, 19, 20));
            item.Add(new Sending("А",true,new DateTime(2024,4,29,19,09,51),1,"Арбузов В.", 1000, 7, 23, 19, 23));
            var eq = item.SequenceEqual(sendings);
        }
    }
}
