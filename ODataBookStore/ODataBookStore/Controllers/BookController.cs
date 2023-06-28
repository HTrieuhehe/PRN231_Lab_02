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
        [EnableQuery(PageSize =2)]
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
            _context.Books.Add(book);
            _context.SaveChanges();
            return Created(book);
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

        [HttpPost("/AccountRegister")]
        public IActionResult AccountRegister
            ([FromBody] RegisterRequest request)
        {
            try
            {
                #region checkRole
                /*
                    đoạn code này thật sự dơ như không còn cách nào khác
                    vì hiện tại nó vẫn còn lỗi lúc update-database chưa biết lỗi sao mà ko tạo được dữ liệu của bảng Role, ai fix đc thì cảm ơn rất nhiều
                    đoạn code này sẽ check nếu bảng Role nó null sẽ tạo dữ liệu với với Id = 1 <=> Account và Id =2 <=> User :)))
                 */

                var checkRole = _context.Roles;
                if (checkRole == null)
                {
                    Role accountRole = new Role();
                    Role userRole = new Role();

                    accountRole.Id = 1;
                    accountRole.RoleName = "Account";

                    userRole.Id = 2;
                    userRole.RoleName = "User";

                    _context.Roles.Add(accountRole);
                    _context.Roles.Add(userRole);
                    _context.SaveChanges();
                }


                #endregion

                var checkAccount = _context.Accounts
                    .FirstOrDefault(x => x.Username.Contains(request.Username));

                if (checkAccount != null)
                {
                    return BadRequest("Username Existed!");
                }

                //convert from string to byte
                Account acc = new Account();
                acc.Username = request.Username;
                acc.Password = Ultils.GetHash(request.Password, _Prn231_Api);

                var role = _context.Roles.FirstOrDefault(x => x.RoleName == "Account");
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

        [HttpPost("/UserRegister")]
        public IActionResult UserRegister
            ([FromBody] RegisterRequest request)
        {
            try
            {
                #region checkRole
                /*
                    đoạn code này thật sự dơ như không còn cách nào khác
                    vì hiện tại nó vẫn còn lỗi lúc update-database chưa biết lỗi sao mà ko tạo được dữ liệu của bảng Role, ai fix đc thì cảm ơn rất nhiều
                    đoạn code này sẽ check nếu bảng Role nó null sẽ tạo dữ liệu với với Id = 1 <=> Account và Id =2 <=> User :)))
                 */
                var checkRole = _context.Roles;
                if (checkRole == null)
                {
                    Role accountRole = new Role();
                    Role userRole = new Role();

                    accountRole.Id = 1;
                    accountRole.RoleName = "Account";

                    userRole.Id = 2;
                    userRole.RoleName = "User";

                    _context.Roles.Add(accountRole);
                    _context.Roles.Add(userRole);
                    _context.SaveChanges();
                }
                #endregion

                var checkAccount = _context.Users
                    .FirstOrDefault(x => x.Username.Contains(request.Username));

                if (checkAccount != null)
                {
                    return BadRequest("Username Existed!");
                }

                //convert from string to byte
                User acc = new User();
                acc.Username = request.Username;
                acc.Password = Ultils.GetHash(request.Password, _Prn231_Api);

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

        [HttpPost("/AccountLogin")]
        public IActionResult AccountLogin
            ([FromBody] LoginRequest request)
        {
            try
            {
                var admin = _context.Accounts.FirstOrDefault(x => x.Username.Contains(request.Username));

                if (admin == null || !Ultils.CompareHash(request.Password, admin.Password, _Prn231_Api))
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

        [HttpPost("/UserLogin")]
        public IActionResult UserLogin
            ([FromBody] LoginRequest request)
        {
            try
            {
                var admin = _context.Users.FirstOrDefault(x => x.Username.Contains(request.Username));

                if (admin == null || !Ultils.CompareHash(request.Password, admin.Password, _Prn231_Api))
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
