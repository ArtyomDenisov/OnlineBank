using Microsoft.AspNetCore.Mvc;
using OnlineBank.Models;
using System.Net.Http;
using System.Text.Json;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OnlineBank.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index() //Login
        {
            return View();
        }

        [HttpPost]
        public IActionResult Check(UserLogIn contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    try
                    {
                        contact.UserPassword = EncryptSHA512(contact.UserPassword);
                        HttpClient httpClient = new HttpClient();

                        httpClient.DefaultRequestHeaders.Add("token", Constants.Token);

                        HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/users/auth?login={contact.UserLogin}&password={contact.UserPassword}").Result;
                        _ = response.EnsureSuccessStatusCode();
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;
                        User? user = JsonSerializer.Deserialize<User>(jsonResponse);

                        ViewData["Auth-Result"] = string.Format("Welcome\\nDate and time:{0}", DateTime.Now.ToString());

                        if (user != null)
                        {
                            if (this.Request.Cookies["user-id"] is null)
                            {
                                this.Response.Cookies.Delete("user-id");

                            }

                            this.Response.Cookies.Append("user-id", user.UserId.ToString());

                            response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/cards?user_id={user.UserId}").Result;

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

                            return Redirect("/home");
                        }
                    }

                    catch (Exception ex)
                    {

                    }
                    
                    try
                    {
                        HttpClient httpClient = new HttpClient();
                        httpClient.DefaultRequestHeaders.Add("token", Constants.Token);
                        HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/{Constants.Version}/admins/auth?login={contact.UserLogin}&password={contact.UserPassword}").Result;
                        _ = response.EnsureSuccessStatusCode();
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;

                        Admin? admin = JsonSerializer.Deserialize<Admin>(jsonResponse);

                        if (admin != null)
                        {
                            if (this.Request.Cookies["admin-id"] is null)
                            {
                                this.Response.Cookies.Delete("admin-id");

                            }

                            this.Response.Cookies.Append("admin-id", admin.AdminId.ToString());
                        }

                        return Redirect("/admin");
                    }

                    catch (Exception ex)
                    {
                        TempData["AlertMessage"] = "Неверный логин или пароль! Пожалуста, повторите вход снова!";
                        return Redirect("/login");
                    }
                }

                catch (Exception ex)
                {
                    TempData["AlertMessage"] = "Неверный логин или пароль! Пожалуста, повторите вход снова!";
                    return Redirect("/login");
                }
            }
            return Redirect("/home");
        }

        public IActionResult Logout()
        {
            this.Response.Cookies.Delete("user-id");
            return Redirect("/home");
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
