using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using System.Net.Http.Headers;

namespace ODataBookStoreWebClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient client = null;

        private readonly string AccountLoginApiUrl = "";
        private readonly string AccountRegisterApiUrl = "";

        private readonly string UserLoginApiUrl = "";
        private readonly string UserRegisterApiUrl = "";

        public LoginController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);


            AccountLoginApiUrl = "https://localhost:44319/Account/Login?";
            AccountRegisterApiUrl = "https://localhost:44319/Account/Register?";

            UserLoginApiUrl = "https://localhost:44319/User/Login?";
            UserRegisterApiUrl = "https://localhost:44319/User/Register?";
        }

        public async Task<IActionResult> Index()
        {
            return View("~/Views/Login/Index.cshtml");
        }

        public async Task<IActionResult> AccountLogin(string username, string password)
        {
            //https://localhost:44319/Account/Login?username=string&password=string

            HttpResponseMessage response 
                = await client.PostAsync("https://localhost:44319/Account/Login?username=" + $"{Uri.EscapeDataString(username)}" + "&password=" + $"{Uri.EscapeDataString(password)}", null);
            
            if (response != null)
            {
                string strData = await response.Content.ReadAsStringAsync();
                TempData["UserData"] = strData;

                return RedirectToAction("", "Book");
            }
            return View("../Login/Index");
        }

    }
}
