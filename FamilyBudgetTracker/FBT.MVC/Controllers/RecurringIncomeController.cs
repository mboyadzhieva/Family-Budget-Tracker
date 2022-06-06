using FBT.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FBT.MVC.Controllers
{
    public class RecurringIncomeController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string apiUrl = "https://familybudgettrackerwebapi.azurewebsites.net/recurringIncomes";
        private const string JSON_MEDIA_TYPE = "application/json";

        public RecurringIncomeController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<IncomeModel> recurringIncomes = new List<IncomeModel>();

            using (var client = new HttpClient())
            {
                var token = httpContextAccessor.HttpContext.Request.Cookies["token"];

                if (token == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                string tokenValue = token.ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON_MEDIA_TYPE));

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode && token != null)
                {
                    //storing the response details received from the web api
                    var recurringIncomesResponse = response.Content.ReadAsStringAsync().Result;
                    //deserializing the response and storing it into the list
                    recurringIncomes = JsonConvert.DeserializeObject<List<IncomeModel>>(recurringIncomesResponse);
                }
            }
            return View(recurringIncomes);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(IncomeModel model)
        {
            using (var client = new HttpClient())
            {
                var token = httpContextAccessor.HttpContext.Request.Cookies["token"];

                if (token == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                string tokenValue = token.ToString();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON_MEDIA_TYPE));

                var response = await client.PostAsJsonAsync(apiUrl, model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error!");
            return View(model);
        }
    }
}
