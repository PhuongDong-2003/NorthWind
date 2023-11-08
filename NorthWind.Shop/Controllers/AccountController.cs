using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWind.Core.Entity;
using NorthWind.Shop.Service;
using NorthWind.Web.Service;

namespace NorthWind.Shop.Controllers
{

    public class AccountController : Controller
    {
        private readonly CustomerService _customerService;

        public AccountController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(Customer customer, bool status)
        {
            
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var result = await _customerService.GetCustomer();
                Console.WriteLine(status);
                var validAccount = result.Where(x => x.CustomerID == customer.CustomerID && x.Email == customer.Email).FirstOrDefault();
                if (validAccount != null)
                {

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, validAccount.CustomerID),
                    new Claim(ClaimTypes.Role, "Administrator"),
                    new Claim("TypeCustomer", "Vip")
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);


                    ViewData["ValidateMessage"] = "Đăng Nhập thành công.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ValidateMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
                }


            }

            return View("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}