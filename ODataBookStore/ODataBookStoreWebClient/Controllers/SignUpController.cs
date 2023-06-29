using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using System.Net.Http.Headers;

namespace ODataBookStoreWebClient.Controllers
{
    public class SignUpController : Controller
    {
        private readonly HttpClient client = null;

        public SignUpController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IActionResult Index()
        {
            return View("~/Views/Login/SignUp.cshtml");
        }

        public async Task<IActionResult> SignUp(string username, string password, int roleId)
        {
            HttpResponseMessage response
                = await client.PostAsync("https://localhost:44319/Account/Register?username=" + $"{Uri.EscapeDataString(username)}" + "&password=" + $"{Uri.EscapeDataString(password)}" + "&roleId=" + $"{roleId}", null);
            if(response.IsSuccessStatusCode)
            {
                return View("~/Views/Login/Index.cshtml");
            }
            return View();
        }
    }
}
