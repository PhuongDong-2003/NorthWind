using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWind.Shop.Service;

namespace NorthWind.Shop.Controllers
{    
    public class Home : Controller
    {
    private readonly ProductService _productService;

        public Home(ProductService productService)
        {
            _productService = productService;
        }

        public async Task <IActionResult> Index(int page =1, int pageSize=10)
        {
             var productResponse = await _productService.GetProductPage(page, pageSize);
            if (productResponse != null)
            {
                ViewData["productlist"] = productResponse;
                return View();
            }
            else
            {

                return View("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}