using Bank.Infrastructure.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Bank.UI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LoginUser(Bank.Infrastructure.DTO.LoginDTO loginDTO)
        {
            //HttpContext.Response.Cookies.Append("Username", loginDTO.UserName.ToString());

            var Item = LoginUserDetails(loginDTO).Result;

            if (Item != -1)
            {
                var userClaims = new List<Claim>()
                {
                   new Claim(ClaimTypes.NameIdentifier,loginDTO.Email),
                   new Claim("UserName","Admin"),
                   new Claim("UserID",Item.ToString()),
                };

                var userIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login");
            }
        }

        public async Task<int> LoginUserDetails(Bank.Infrastructure.DTO.LoginDTO loginDTO)
        {
            HttpClient client = new HttpClient();
            string url = "http://localhost:7105/";

            var LoginContextDTO = JsonConvert.SerializeObject(loginDTO);

            var response = await client.PostAsync(url + "api/User/Login",
                new StringContent(LoginContextDTO, Encoding.UTF8, "application/json"));
            var Data = await response.Content.ReadAsStringAsync();
            int id = JsonConvert.DeserializeObject<int>(Data);

            return id;
        }
        public async Task<IActionResult> LogOut()
        {

            var _user = HttpContext.User as ClaimsPrincipal;
            var _identity = _user.Identity as ClaimsIdentity;

            foreach (var claim in _user.Claims.ToList())
            {
                _identity.RemoveClaim(claim);
            }

            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

























    }









}
