using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBank.Models;
using System.Text;
using System.Text.Json;

namespace OnlineBank.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("token", Constants.Token);
            HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/1.0/card-variants").Result;
            _ = response.EnsureSuccessStatusCode();
            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            List<CardVariant>? cardVariants = JsonSerializer.Deserialize<List<CardVariant>>(jsonResponse);
            return View(cardVariants);
        }

        public ActionResult Details(int id)
        {
            HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("token", Constants.Token);
            HttpResponseMessage response = httpClient.GetAsync($"http://habar-bank-api3.somee.com/api/1.0/card-variants/{id}").Result;
            _ = response.EnsureSuccessStatusCode();
            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            CardVariant? cardVariant = JsonSerializer.Deserialize<CardVariant>(jsonResponse);
            return View(cardVariant);
        }

        public IActionResult AddCardVariant()
        {
            return View();
        }

        public ActionResult Create(CardVariant cardVariant)
        {
            try
            {
                using StringContent jsonContent = new(
                JsonSerializer.Serialize(cardVariant),
                Encoding.UTF8,
                "application/json");

                HttpClient httpClient = new();
                httpClient.DefaultRequestHeaders.Add("token", Constants.Token);
                HttpResponseMessage response = httpClient.PostAsync($"http://habar-bank-api3.somee.com/api/1.0/card-variants/", jsonContent).Result;
                return Redirect("/admin");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult RemoveCardVariant()
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Add("token", Constants.Token);
            HttpResponseMessage response = httpClient.PutAsync($"http://habar-bank-api3.somee.com/api/1.0/card-variants/{id}?enabled=false", null).Result;
            return Redirect("/admin");
        }
    }
}
