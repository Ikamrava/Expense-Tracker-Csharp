using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Expense_Traker_Csharp.Models
{

    public class AccountController : Controller
    {

        public IActionResult Login()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "https://ikam-expense.azurewebsites.net/Dashboard");
            }
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(VMLogin modelLogin)
        {
            DotNetEnv.Env.Load();
            if (modelLogin.Email == Environment.GetEnvironmentVariable("EMAIL") && modelLogin.Password == Environment.GetEnvironmentVariable("PASSWORD"))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties", "Role")

                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.RememberMe,

                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index", "https://ikam-expense.azurewebsites.net/Dashboard");
            }
            ViewData["ValidateMessage"] = "Invalid Email or Password";



            return View();
        }



    }
}