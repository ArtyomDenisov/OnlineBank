using Microsoft.AspNetCore.Mvc;
using OnlineBank.Models;
using System.Net.Http;
using System.Text.Json;
using System.Xml.Linq;

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
                    HttpClient httpClient = new HttpClient();

                    HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/users/auth?login={contact.UserLogin}&password={contact.UserPassword}").Result;

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
                        
                        response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/cards?user_id={user.UserId}").Result;

                        _ = response.EnsureSuccessStatusCode();

                        jsonResponse = response.Content.ReadAsStringAsync().Result;

                        List<Card>? cards = JsonSerializer.Deserialize<List<Card>>(jsonResponse);

                        foreach (Card card in cards)
                        {
                            if (card != null)
                            {
                                if ( this.Request.Cookies["card-id"] is null)
                                {
                                    this.Response.Cookies.Delete("card-id");
                                }
                            }
                            this.Response.Cookies.Append("card-id", card.SubstanceId.ToString());
                        }
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
    }
}
