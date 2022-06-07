namespace FBT.MVC.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models;
    using Newtonsoft.Json;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string apiUrl = "https://familybudgettrackerwebapi.azurewebsites.net/budget";
        private const string JSON_MEDIA_TYPE = "application/json";

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var budget = new BudgetModel();

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
                    var budgetResponse = response.Content.ReadAsStringAsync().Result;
                    
                    budget = JsonConvert.DeserializeObject<BudgetModel>(budgetResponse);
                }
            }

            return View(budget);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
