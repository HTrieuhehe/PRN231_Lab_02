using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ODataBookStore.Models;
using ODataBookStore.Models.Request;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;

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

            ProductApiUrl = "https://localhost:44319/odata/Book";
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

        //[HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (book == null)
            {
                return View("~/Views/Book/Create.cshtml");
            }

            string jsonString = JsonSerializer.Serialize(book);
            HttpResponseMessage response = await client.PostAsync("https://localhost:44319/odata/" + $"{book}", null);

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Book");
            }
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(IFormCollection collection)
        //{

        //}


        public async Task<IActionResult> Edit(int id)
        {
           // HttpResponseMessage response = await client.GetAsync(ProductApiUrl + $"/{id}?$expand=Location,Press");
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + $"/{id}");
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
            };

            return View(book);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, IFormCollection collection)
        //{

        //}


        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + $"/{id}");
            string strData = await response.Content.ReadAsStringAsync();
            dynamic temp = JObject.Parse(strData);

            return View(temp);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id, IFormCollection collection)
        //{

        //}
    }
}
