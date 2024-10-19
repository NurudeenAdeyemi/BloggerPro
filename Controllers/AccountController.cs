using BloggerPro.Models;
using BloggerPro.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;

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
            if(user is not null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Invalid credential";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
