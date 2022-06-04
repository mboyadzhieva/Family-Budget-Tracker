namespace FBT.MVC.Controllers
{
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Http;
    using System.Net.Http.Json;

    public class AccountController : Controller
    {
        private readonly string apiUrl = "https://localhost:5001/identity";

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            using (var client = new HttpClient())
            {
                var postLogin = client.PostAsJsonAsync($"{apiUrl}/login", model);
                postLogin.Wait();

                var result = postLogin.Result;
                if (result.IsSuccessStatusCode)
                {
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
