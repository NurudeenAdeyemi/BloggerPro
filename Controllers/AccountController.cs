using BloggerPro.Models;
using BloggerPro.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloggerPro.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {          
            UserRepository.Register(user);
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = UserRepository.Login(username, password);
            if (user == null)
            {
                ViewBag.Message = "Invalid Username/Password";
                return View();
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.GivenName, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
