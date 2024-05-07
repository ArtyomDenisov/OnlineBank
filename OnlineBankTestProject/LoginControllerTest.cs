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
using System.Security.Cryptography;

namespace OnlineBankTestProject
{
    [TestClass]
    public class LoginControllerTest
    {
        [TestMethod]
        public void SuccessLogin()
        {
            HttpClient httpClient = new HttpClient();
            UserLogIn userLogIn = new UserLogIn();

            userLogIn.UserLogin = "ArtyomDen";
            userLogIn.UserPassword = "abababab";

            userLogIn.UserPassword = EncryptSHA512(userLogIn.UserPassword);

            httpClient.DefaultRequestHeaders.Add("token", Constants.Token);
            HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/users/auth?login={userLogIn.UserLogin}&password={userLogIn.UserPassword}").Result;

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
        public void FailedLogin()
        {
            HttpClient httpClient = new HttpClient();
            UserLogIn userLogIn = new UserLogIn();

            userLogIn.UserLogin = "ArtyomDen";
            userLogIn.UserPassword = "abababab1";

            userLogIn.UserPassword = EncryptSHA512(userLogIn.UserPassword);

            httpClient.DefaultRequestHeaders.Add("token", Constants.Token);
            HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/users/auth?login={userLogIn.UserLogin}&password={userLogIn.UserPassword}").Result;

            string jsonResponse = response.Content.ReadAsStringAsync().Result;

            Assert.AreEqual(jsonResponse, "Аккаунт с указанным логином и паролём не найден");

        }

        internal string EncryptSHA512(string value)
        {
            using SHA512 sha512Hash = SHA512.Create();

            byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

            StringBuilder builder = new();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
