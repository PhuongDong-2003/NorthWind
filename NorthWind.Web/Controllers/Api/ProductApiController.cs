using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWind.Core.Entity;
using NorthWind.Web.Service;

namespace NorthWind.Web.Controllers.Api
{
    
    public class ProductApiController : Controller
    {
        private readonly ILogger<ProductApiController> _logger;
        private readonly ProductService _productService;

        public ProductApiController(ILogger<ProductApiController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult>  GetByPage()
        {
            var productResponse = await _productService.GetProduct();
            return Json(productResponse);
        }

        [HttpPost]
        public async Task  Create([FromBody] Product product)
        {
            await _productService.InsertProduct(product);
        }

        [HttpPost]
        public async Task  Update([FromBody] Product product)
        {
            await _productService.UpdateProduct(product);
        }

        [HttpPost]
        public async Task  Delete([FromBody] int productId)
        {
            await _productService.DeleteProduct(productId);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}