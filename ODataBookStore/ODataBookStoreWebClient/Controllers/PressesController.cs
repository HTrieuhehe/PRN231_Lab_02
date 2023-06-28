using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace ODataBookStoreWebClient.Controllers
{
    public class PressesController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public PressesController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7057/odata/Book";
        }

        //GET: Book Controller
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            var list = temp.value;
            List<Press> items = ((JArray)list).Select(x => new Press
            {
                Id = (int)x["Id"],
                Name = (string)x["Name"]
            }).ToList();
            return View(items);
        }
    }
}
