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

namespace OnlineBankTestProject
{
    [TestClass]
    public class CardControllerTest
    {
        [TestMethod]
        public void SuccessNewCard()
        {
            Card card = new Card();
            card.AccountId = 6;
            card.Enabled = true;
            card.RublesCount = 1000;
            card.ImagePath = "CardDesign4empty.png";

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(card),
                Encoding.UTF8,
                "application/json");

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("token", Constants.Token);
            HttpResponseMessage response = httpClient.PostAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/cards?user_id={card.AccountId}", jsonContent).Result;

            Assert.AreEqual(response.IsSuccessStatusCode, true);
        }
    }
}
