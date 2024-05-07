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

namespace OnlineBankTestProject
{
    [TestClass]
    public class TransControllerTest
    {
        [TestMethod]
        public void SuccessTrans()
        {
            int userId = 6;
            bool isCardExist = false;
            Sending sending = new Sending();
            sending.SubstanceId = 24;
            sending.CardNumber = "2202 2059 4146 1433";
            sending.RublesCount = 1;

            HttpClient httpClient = new HttpClient();


            httpClient.DefaultRequestHeaders.Add("token", Constants.Token);

            HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/cards?user_id={userId}").Result;
            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            List<Card> userCards = JsonSerializer.Deserialize<List<Card>>(jsonResponse);

            foreach (Card userCard in userCards)
            {
                if (userCard.SubstanceId == sending.SubstanceId)
                {
                    isCardExist = true;
                    if (userCard.RublesCount < sending.RublesCount)
                    {
                        throw new Exception();
                    }
                    break;
                }
            }

            if (!isCardExist)
            {
                throw new Exception();
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
                List<Card> newCards = JsonSerializer.Deserialize<List<Card>>(jsonResponse);
                foreach (Card newCard in newCards)
                {
                    cards.Add(newCard);
                }

            }

            sending.CardNumber = sending.CardNumber.Remove(0, 4);
            Card card = cards.Find(x => x.CardNumber.Remove(0, 4).Contains(sending.CardNumber));

            if (card is null)
            {
                throw new Exception();
            }

            using StringContent jsonContent = new(
                    JsonSerializer.Serialize(sending),
                    Encoding.UTF8,
                    "application/json");

            response = httpClient.PostAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/transfers", jsonContent).Result;

            Assert.AreEqual(response.IsSuccessStatusCode, true);
        }
    }
}
