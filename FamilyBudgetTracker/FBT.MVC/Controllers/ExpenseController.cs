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
    public class ExpenseController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string apiUrl = "https://familybudgettrackerwebapi.azurewebsites.net";
        private const string JSON_MEDIA_TYPE = "application/json";

        public ExpenseController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<ExpenseModel> expenses = new List<ExpenseModel>();

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

                var expensesPostRequestResponse = await client.GetAsync($"{apiUrl}/expenses");
                var recurringExpensesPostRequestResponse = await client.GetAsync($"{apiUrl}/recurringExpenses");

                if (expensesPostRequestResponse.IsSuccessStatusCode 
                    && recurringExpensesPostRequestResponse.IsSuccessStatusCode)
                {
                    var expensesResult = expensesPostRequestResponse.Content.ReadAsStringAsync().Result;
                    var recurringExpensesResult = recurringExpensesPostRequestResponse.Content.ReadAsStringAsync().Result;

                    JsonConvert.DeserializeObject<List<ExpenseModel>>(expensesResult).ForEach(e =>
                    {
                        e.IsRecurring = false;
                        expenses.Add(e);
                    });

                    JsonConvert.DeserializeObject<List<ExpenseModel>>(recurringExpensesResult).ForEach(re =>
                    {
                        re.IsRecurring = true;
                        expenses.Add(re);
                    });
                }
            }
            return View(expenses);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ExpenseModel model)
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

                HttpResponseMessage response = new HttpResponseMessage();

                if (model.IsRecurring)
                {
                    response = await client.PostAsJsonAsync($"{apiUrl}/recurringExpenses", model);
                }
                else
                {
                    response = await client.PostAsJsonAsync($"{apiUrl}/expenses", model);
                }

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error!");
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(ExpenseModel model)
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

                HttpResponseMessage response = new HttpResponseMessage();

                if (model.IsRecurring)
                {
                    response = await client.PostAsJsonAsync($"{apiUrl}/recurringExpenses", model.Id);
                }
                else
                {
                    response = await client.PostAsJsonAsync($"{apiUrl}/expenses", model.Id);
                }

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error!");
            return View("~/Views/Expense/Create.cshtml", model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ExpenseModel model)
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

                HttpResponseMessage response = new HttpResponseMessage();

                if (model.IsRecurring)
                {
                    response = await client.PostAsJsonAsync($"{apiUrl}/recurringExpenses", model);
                }
                else
                {
                    response = await client.PostAsJsonAsync($"{apiUrl}/expenses", model);
                }

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
