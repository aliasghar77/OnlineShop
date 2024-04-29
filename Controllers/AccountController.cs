using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineShop.Models;
using System.Security.Claims;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountViewModel Register)
        {
            if(!ModelState.IsValid)
            {
                return View(Register);
            }
            
            if(_userRepository.IsExistUserByEmail(Register.Email))
            {
                ModelState.AddModelError("Email", "این ایمیل قبلا ثبت شده است");
                return View(Register);
            }

            Users user = new Users()
            {
                Email = Register.Email.ToLower(),
                Password = Register.Password,
                RegisterDate = DateTime.Now,
                IsAdmin = false

            };
            _userRepository.AddUser(user);

            return View("SuccessRegister" , Register);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if(!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userRepository.GetUserForLogin(login.Email.ToLower(), login.Password);
            if (user == null)
            {
                 ModelState.AddModelError("Email", "اطلاعات صحبح نیست");
                return View(login);


            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
               // new Claim("CodeMeli", user.Email),

            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };

            HttpContext.SignInAsync(principal, properties);

            return Redirect("/");

            
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }
    }
}
