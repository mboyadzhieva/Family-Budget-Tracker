namespace FBT.MVC.Controllers
{
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Http;
    using System.Net.Http.Json;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

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
                    var loginResponse = response.Content.ReadAsStringAsync().Result;

                    TokenModel tokenModel = JsonConvert.DeserializeObject<TokenModel>(loginResponse);

                    CookieOptions options = new CookieOptions();
                    Response.Cookies.Append("token", tokenModel.Token);

                    //httpContextAccessor.HttpContext.Request.Cookies["token"] = token;

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
        public ActionResult Register(RegisterModel model)
        {
            using (var client = new HttpClient())
            {
                var postRegister = client.PostAsJsonAsync($"{apiUrl}/register", model);
                postRegister.Wait();

                var result = postRegister.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error!");
            return View(model);
        }
    }
}
