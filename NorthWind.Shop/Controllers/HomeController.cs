using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWind.Core.Entity;
using NorthWind.Shop.Service;

namespace NorthWind.Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;

        public HomeController(ProductService productService)
        {
            _productService = productService;
        }
        public static bool status = true;
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {

            // if (HttpContext.User.Identity.IsAuthenticated)
            // {
            //     HttpContext.Session.SetString("CMND", "123456");

            //     if (HttpContext.User.Claims.First(x => x.Type == "TypeCustomer").Value == "Vip")
            //     {
            //         HttpContext.Response.Cookies.Append("IsVip", "1");
            //     }
            // }
            
            var productResponse = await _productService.GetProductPage(page, pageSize);
            if (productResponse != null)
            {
                ViewData["productlist"] = productResponse;
                ViewData["st"] = status;
                string message = TempData["messAddCart"] as string;
                if (!string.IsNullOrEmpty(message))
                {
                    ViewData["messAddCart"] = message; 
                }
                return View();
            }
            else
            {

                return View("Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Reset(int page = 1, int pageSize = 10)
        {
            status = true;
            await Index(page, pageSize);

            return View("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Find(string productName)
        {
            status = false;
            ViewData["st"] = status;

            if (productName != null)
            {
                var product = await _productService.GetByName(productName);
                ViewData["product"] = product;

            }
            else
            {
                Console.WriteLine("Không nhận được ID");
            }
            return View("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public IActionResult About() => View();
    }
}