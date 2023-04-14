using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.Account;
using MVC.Repository;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        private IAccountManagerRepository accountManagerRepository;

        public LoginController(IAccountManagerRepository accountManagerRepository)
        {
            this.accountManagerRepository = accountManagerRepository;
        }

        public IActionResult Login([FromQuery] string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] UserAccount userAccount, [FromQuery] string? returnUrl)
        {
            if (ModelState.IsValid == false)
                return View(userAccount);

            var result = this.accountManagerRepository.Login(userAccount.Email, userAccount.Password).Result;

            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl) == false)
                    return Redirect(returnUrl);

                return Redirect("/Home/Index");
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