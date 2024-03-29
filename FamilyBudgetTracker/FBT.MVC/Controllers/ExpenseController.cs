﻿namespace FBT.MVC.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class ExpenseController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string apiUrl = "https://family-budget-tracker-api.herokuapp.com";
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

                // check to see if a JWT is present and if not, redirect to login
                if (token == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                string tokenValue = token.ToString();

                // add authorization header and content type headers to the HTTP request
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON_MEDIA_TYPE));

                // get all expenses (one-time and recurring)
                var expensesPostRequestResponse = await client.GetAsync($"{apiUrl}/expenses");
                var recurringExpensesPostRequestResponse = await client.GetAsync($"{apiUrl}/recurringExpenses");

                // if the response is successfull, mark the recurring payments as such
                if (expensesPostRequestResponse.IsSuccessStatusCode
                    && recurringExpensesPostRequestResponse.IsSuccessStatusCode)
                {
                    var expensesResult = expensesPostRequestResponse.Content.ReadAsStringAsync().Result;
                    var recurringExpensesResult = recurringExpensesPostRequestResponse.Content.ReadAsStringAsync().Result;

                    JsonConvert.DeserializeObject<List<ExpenseModel>>(expensesResult).ForEach(e =>
                    {
                        e.IsRecurring = false;
                        if (e.PaymentDate <= DateTime.UtcNow)
                        {
                            e.TotalAmount = e.Amount;
                        }
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
            return View("~/Views/Expense/Form.cshtml");
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

                // send the newly created data to an API endpoint, depending on whether it's recurring or not
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
            return View("~/Views/Expense/Form.cshtml", model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id, bool isRecurring)
        {
            ExpenseModel expense = new ExpenseModel();

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

                // first step for editing an expense is getting all of its information
                if (isRecurring)
                {
                    response = await client.GetAsync($"{apiUrl}/recurringExpenses/{id}");
                }
                else
                {
                    response = await client.GetAsync($"{apiUrl}/expenses/{id}");
                }

                if (response.IsSuccessStatusCode)
                {
                    var expenseResult = response.Content.ReadAsStringAsync().Result;

                    expense = JsonConvert.DeserializeObject<ExpenseModel>(expenseResult);
                }
            }

            return View("~/Views/Expense/Form.cshtml", expense);
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

                // sending PUT request to the appropriate endpoint
                if (model.IsRecurring)
                {
                    response = await client.PutAsJsonAsync($"{apiUrl}/recurringExpenses", model);
                }
                else
                {
                    response = await client.PutAsJsonAsync($"{apiUrl}/expenses", model);
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

                // first step is getting the right expense and then sending a HTTP Delete request
                if (isRecurring)
                {
                    response = await client.GetAsync($"{apiUrl}/recurringExpenses/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        await client.DeleteAsync($"{apiUrl}/recurringExpenses/{id}");
                    }
                }
                else
                {
                    response = await client.GetAsync($"{apiUrl}/expenses/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        await client.DeleteAsync($"{apiUrl}/expenses/{id}");
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}
