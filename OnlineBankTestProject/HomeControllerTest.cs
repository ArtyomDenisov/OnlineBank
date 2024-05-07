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
    public  class HomeControllerTest
    {
        [TestMethod]
        public void SuccessUserInfo()
        {
            int userId = 8;

            HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("token", Constants.Token);
            HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/users/{userId}").Result;
            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            User user = JsonSerializer.Deserialize<User>(jsonResponse);

            User CheckUser = new User();

            CheckUser.UserId = 8;
            CheckUser.UserPhone = "+79081003456";
            CheckUser.UserLogin = "ArtyomDen";
            CheckUser.UserPassword = "91c28da564856b5bb4bf2acfffcc56efa566358faff4c1a1ba5603826eaa81a612aa9744e620a84d3f7e742107af70b3ffa0e9b16af8ff977951a6bdf6adda9c";
            CheckUser.UserName = "Артем";
            CheckUser.UserSurname = "Денисов";
            CheckUser.UserPatronymic = "Дмитриевич";
            CheckUser.UserEnabled = true;
            CheckUser.UserUserLevelId = 1;

            Assert.AreEqual(user.UserId, CheckUser.UserId);
            Assert.AreEqual(user.UserPhone, CheckUser.UserPhone);
            Assert.AreEqual(user.UserLogin, CheckUser.UserLogin);
            Assert.AreEqual(user.UserPassword, CheckUser.UserPassword);
            Assert.AreEqual(user.UserName, CheckUser.UserName);
            Assert.AreEqual(user.UserSurname, CheckUser.UserSurname);
            Assert.AreEqual(user.UserPatronymic, CheckUser.UserPatronymic);
            Assert.AreEqual(user.UserEnabled, CheckUser.UserEnabled);
            Assert.AreEqual(user.UserUserLevelId, CheckUser.UserUserLevelId);

        }

        [TestMethod]
        public void SuccessCardInfo()
        {
            int userId = 8;

            HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("token", Constants.Token);
            HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/cards?user_id={userId}").Result;
            string jsonResponse = response.Content.ReadAsStringAsync().Result;

            List<Card> cards = JsonSerializer.Deserialize<List<Card>>(jsonResponse);

            List<Card> item = new List<Card>();
            item.Add(new Card(14, 1, 8, 900, "CardDesign4empty.png", true, "2202-0093-7990-8137"));
            item.Add(new Card(15, 2, 8, 1000, "Files/b39adc122cf8d4ff915a727353264292.jpg", true, "9870-2907-5918-9018"));
            var eq = item.SequenceEqual(cards);
        }
    }
}
