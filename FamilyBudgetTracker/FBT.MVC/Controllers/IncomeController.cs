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
    public class IncomeController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string apiUrl = "https://familybudgettrackerwebapi.azurewebsites.net";
        private const string JSON_MEDIA_TYPE = "application/json";

        public IncomeController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<IncomeModel> incomes = new List<IncomeModel>();

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

                var incomesPostRequestResponse = await client.GetAsync($"{apiUrl}/incomes");
                var recurringIncomesPostRequestResponse = await client.GetAsync($"{apiUrl}/recurringIncomes");

                if (incomesPostRequestResponse.IsSuccessStatusCode
                    && recurringIncomesPostRequestResponse.IsSuccessStatusCode)
                {
                    var incomesResult = incomesPostRequestResponse.Content.ReadAsStringAsync().Result;
                    var recurringIncomesResult = recurringIncomesPostRequestResponse.Content.ReadAsStringAsync().Result;

                    JsonConvert.DeserializeObject<List<IncomeModel>>(incomesResult).ForEach(i =>
                    {
                        i.IsRecurring = false;
                        incomes.Add(i);
                    });

                    JsonConvert.DeserializeObject<List<IncomeModel>>(recurringIncomesResult).ForEach(ri =>
                    {
                        ri.IsRecurring = true;
                        incomes.Add(ri);
                    });
                }
            }
            return View(incomes);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View("~/Views/Income/Form.cshtml");
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

                HttpResponseMessage response = new HttpResponseMessage();

                if (model.IsRecurring)
                {
                    response = await client.PostAsJsonAsync($"{apiUrl}/recurringIncomes", model);
                }
                else
                {
                    response = await client.PostAsJsonAsync($"{apiUrl}/incomes", model);
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
        public async Task<ActionResult> Edit(int id, bool isRecurring)
        {
            IncomeModel income = new IncomeModel();

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

                if (isRecurring)
                {
                    response = await client.GetAsync($"{apiUrl}/recurringIncomes/{id}");
                }
                else
                {
                    response = await client.GetAsync($"{apiUrl}/incomes/{id}");
                }

                if (response.IsSuccessStatusCode)
                {
                    var incomeResult = response.Content.ReadAsStringAsync().Result;

                    income = JsonConvert.DeserializeObject<IncomeModel>(incomeResult);
                }
            }

            //ModelState.AddModelError(string.Empty, "Server Error!");
            return View("~/Views/Income/Form.cshtml", income);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(IncomeModel model)
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
                    response = await client.PutAsJsonAsync($"{apiUrl}/recurringIncomes", model);
                }
                else
                {
                    response = await client.PutAsJsonAsync($"{apiUrl}/incomes", model);
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
        public async Task<ActionResult> Delete(int id, bool isRecurring)
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

                if (isRecurring)
                {
                    response = await client.GetAsync($"{apiUrl}/recurringIncomes/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        await client.DeleteAsync($"{apiUrl}/recurringIncomes/{id}");
                    }
                }
                else
                {
                    response = await client.GetAsync($"{apiUrl}/incomes/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        await client.DeleteAsync($"{apiUrl}/incomes/{id}");
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}
