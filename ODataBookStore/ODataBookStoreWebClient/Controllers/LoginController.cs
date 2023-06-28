using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ODataBookStoreWebClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient client = null;
        private string LoginApiUrl = "";

        public LoginController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            LoginApiUrl = "";
        }

        public async Task<IActionResult> Index()
        {
            return View("~/Views/Login/Index.cshtml");
        }

        //public async Task<IActionResult> AdminLogin()
        //{

        //}

        public async Task<IActionResult> Login(string username, string password)
        {
            return BadRequest();
        }
    }
}
