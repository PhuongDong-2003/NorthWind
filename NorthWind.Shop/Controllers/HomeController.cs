using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            var productResponse = await _productService.GetProductPage(page, pageSize);
            if (productResponse != null)
            {
                ViewData["productlist"] = productResponse;
                ViewData["st"] = status;
                return View();
            }
            else
            {

                return View("Error");
            }
        }
        
        [HttpPost]
        public async  Task<IActionResult> Reset(int page = 1, int pageSize = 10)
        {
            status = true;
            await Index(page, pageSize);
            return View("Index");
           
        }
         [HttpPost]
        public async Task<IActionResult> Find(int productID)
        {
            status = false;
            ViewData["st"] = status;
            int ProductID = int.Parse(Request.Form["productID"]);
            if(productID>0)
            {
                var product = await _productService.GetByID(ProductID);
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
    }
}