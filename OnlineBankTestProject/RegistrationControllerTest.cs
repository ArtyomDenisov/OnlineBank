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
    public class RegistrationControllerTest
    {
        [TestMethod]
        public void SuccessRegistration()
        {
            HttpClient httpClient = new();
            User user = new User();

            user.UserPhone = "+79631006789";
            user.UserLogin = "ArtyomFit";
            user.UserPassword = "12341234";
            user.UserName = "Артем";
            user.UserSurname = "Виталов";
            user.UserPatronymic = "Горевич";
            user.UserEnabled = true;
            user.UserUserLevelId = 1;

            using StringContent jsonContent = new(
            JsonSerializer.Serialize(user),
            Encoding.UTF8,
            "application/json");

            httpClient.DefaultRequestHeaders.Add("token", Constants.Token);
            HttpResponseMessage response = httpClient.PostAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/users/", jsonContent).Result;

            Assert.AreEqual(response.IsSuccessStatusCode, true);
        }
    }
}