namespace FBT.MVC.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string apiUrl = "https://familybudgettrackerwebapi.azurewebsites.net/identity";

        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync($"{apiUrl}/login", model);

                if (response.IsSuccessStatusCode)
                {
                    //storing the response details received from the web api
                    var loginResponseResult = response.Content.ReadAsStringAsync().Result;

                    // extracting token value
                    TokenModel tokenModel = JsonConvert.DeserializeObject<TokenModel>(loginResponseResult);

                    // saving token value in a cookie, that's stored in the browser
                    Response.Cookies.Append("token", tokenModel.Token);

                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error!");
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            using (var client = new HttpClient())
            {
                // sending user data to the API
                var response = await client.PostAsJsonAsync($"{apiUrl}/register", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error!");
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            // removing the saved cookie and redirecting user to login page
            if (httpContextAccessor.HttpContext.Request.Cookies["token"] != null)
            {
                Response.Cookies.Delete("token");
                return RedirectToAction("Login");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
