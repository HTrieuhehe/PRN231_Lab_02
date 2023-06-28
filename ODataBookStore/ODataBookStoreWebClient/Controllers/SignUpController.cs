using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ODataBookStoreWebClient.Controllers
{
    public class SignUpController : Controller
    {
        private readonly HttpClient client = null;
        private string LoginApiUrl = "";

        public SignUpController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            LoginApiUrl = "";
        }

        public IActionResult Index()
        {
            return View("~/Views/Login/SignUp.cshtml");
        }

        public async Task<IActionResult> SignUp(string username, string password)
        {
            return BadRequest();
        }
    }
}
