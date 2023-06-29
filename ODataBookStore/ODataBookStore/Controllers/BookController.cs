using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataBookStore.Models;
using ODataBookStore.Models.Request;
using ODataBookStore.Service.Helpers;
using ODataBookStore.Utilities;

namespace ODataBookStore.Controllers
{
    public class BookController : ODataController
    {
        private BookStoreContext _context;
        private readonly string _Prn231_Api;
        private readonly IConfiguration _configuration;

        public BookController(BookStoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _Prn231_Api = _configuration["PRN231"];
        }
        [EnableQuery(PageSize = 10)]
        public IActionResult Get()
        {
            return Ok(_context.Books.Include(b=>b.Location).Include(b=>b.Press).AsQueryable());
        }
        [EnableQuery]
        public IActionResult Get(int key, string version)
        {
            var book = _context.Books.Include(b => b.Location).Include(b => b.Press).FirstOrDefault(b => b.Id == key);
            if (book == null)
                return NotFound();
            return Ok(book);
        }
        [EnableQuery]
        public IActionResult Post([FromBody] Book book)
        {
           if (book != null)
           {
                _context.Books.Add(book);
                _context.SaveChanges();
                return Created(book);
           }
           return BadRequest();
        }
        [EnableQuery]
        public IActionResult Delete([FromBody]int key)
        {
            Book b = _context.Books.FirstOrDefault(b => b.Id == key);
            if (b == null)
                return NotFound();
            _context.Books.Remove(b);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("/Account/Register")]
        public IActionResult AccountRegister
            ([FromQuery] string username, [FromQuery] string password, [FromQuery] int roleId)
        {
            try
            {
                var checkAccount = _context.Accounts
                    .FirstOrDefault(x => x.Username.Contains(username));

                if (checkAccount != null)
                {
                    return BadRequest("Username Existed!");
                }

                //convert from string to byte
                Account acc = new Account();
                acc.Username = username;
                acc.Password = Ultils.GetHash(password, _Prn231_Api);

                var role = _context.Roles.FirstOrDefault(x => x.Id == roleId);
                if(role == null)
                {
                    return BadRequest();
                }

                acc.RoleId = role.Id;  
                
                _context.Accounts.Add(acc);
                _context.SaveChanges();

                return Ok(acc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/User/Register")]
        public IActionResult UserRegister
            ([FromQuery] string username, [FromQuery] string password)
        {
            try
            {
                var checkAccount = _context.Users
                    .FirstOrDefault(x => x.Username.Contains(username));

                if (checkAccount != null)
                {
                    return BadRequest("Username Existed!");
                }

                //convert from string to byte
                User acc = new User();
                acc.Username = username;
                acc.Password = Ultils.GetHash(password, _Prn231_Api);

                var role = _context.Roles.FirstOrDefault(x => x.RoleName == "User");
                if (role == null)
                {
                    return BadRequest();
                }

                acc.RoleId = role.Id;

                _context.Users.Add(acc);
                _context.SaveChanges();

                return Ok(acc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/Account/Login")]
        public IActionResult AccountLogin
            ([FromQuery] string username, [FromQuery] string password)
        {
            try
            {
                var admin = _context.Accounts.FirstOrDefault(x => x.Username.Contains(username));

                if (admin == null || !Ultils.CompareHash(password, admin.Password, _Prn231_Api))
                {
                    return BadRequest();
                }

                //Create JWT Token

                //--- string.IsNullOrEmpty(admin.Username) ? "" : admin.Username ---
                //là một cách viết tắt của câu lệnh điều kiện if-else. Nó kiểm tra xem giá trị của biến admin.Username có là chuỗi rỗng hoặc null hay không
                var newToken = AccessTokenManager.GenerateJwtToken(string.IsNullOrEmpty(admin.Username) ? "" : admin.Username, admin.RoleId, admin.Id, _configuration);

                return Ok(newToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/User/Login")]
        public IActionResult UserLogin
            ([FromQuery] string username, [FromQuery] string password)
        {
            try
            {
                var admin = _context.Users.FirstOrDefault(x => x.Username.Contains(username));

                if (admin == null || !Ultils.CompareHash(password, admin.Password, _Prn231_Api))
                {
                    return BadRequest();
                }

                //Create JWT Token

                //--- string.IsNullOrEmpty(admin.Username) ? "" : admin.Username ---
                //là một cách viết tắt của câu lệnh điều kiện if-else. Nó kiểm tra xem giá trị của biến admin.Username có là chuỗi rỗng hoặc null hay không
                var newToken = AccessTokenManager.GenerateJwtToken(string.IsNullOrEmpty(admin.Username) ? "" : admin.Username, admin.RoleId, admin.Id, _configuration);

                return Ok(newToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
