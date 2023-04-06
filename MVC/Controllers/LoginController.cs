using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.Account;
using MVC.Repository;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        private IAccountManagerRepository accountManagerRepository;

        public LoginController(IAccountManagerRepository accountManagerRepository)
        {
            this.accountManagerRepository = accountManagerRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] UserAccount userAccount)
        {
            if (ModelState.IsValid == false)
                return View(userAccount);

            var result = this.accountManagerRepository.Login(userAccount.Email, userAccount.Password).Result;

            if (result.Succeeded)
            {
                return Redirect("http://localhost:3000");
            }

            ViewBag.LoginError = "Email/Password inválidos";

            return View(userAccount);
        }

        public IActionResult Logout()
        {
            this.accountManagerRepository.Logout();

            return RedirectToAction("Login");
        }
    }
}