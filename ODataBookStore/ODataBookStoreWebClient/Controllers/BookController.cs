using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using ODataBookStore.Models.Request;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace ODataBookStoreWebClient.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient client = null;
        private readonly string ProductApiUrl = "";

        public BookController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            ProductApiUrl = "https://localhost:7057/odata/Book";
        }
          
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            var list = temp.value;
            List<Book> items = ((JArray)list).Select(x => new Book
            {
                Id = (int)x["Id"],
                Author = (string)x["Author"],
                ISBN = (string)x["ISBN"],
                Title = (string)x["Title"],
                Price = (decimal)x["Price"]
            }).ToList();
            return View(items);
        }
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + $"/{id}?$expand=Location,Press");
            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);
            Book book = new Book
            {
                Id = temp.Id,
                ISBN = temp.ISBN,
                Title = temp.Title,
                Author = temp.Author,
                Price = temp.Price,
                LocationName = temp.LocationName,
                PressId = temp.PressId,
                Location = new Address
                {
                    City = temp.Location.City,
                    Street = temp.Location.Street
                },
                Press = new Press
                {
                    Id = temp.Press.Id,
                    Name = temp.Press.Name,
                    Category = temp.Press.Category
                }
            };
            return View(book);
               
        }

        

        //public IActionResult Create()
        //{

        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(IFormCollection collection)
        //{

        //}


        //public IActionResult Edit(int id)
        //{

        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, IFormCollection collection)
        //{

        //}


        //public IActionResult Delete(int id)
        //{

        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id, IFormCollection collection)
        //{

        //}
    }
}
