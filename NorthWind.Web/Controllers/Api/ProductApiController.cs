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
            if(productResponse != null)
            {
            _logger.LogInformation("Load Prouducts");
            return Json(productResponse);
            }
            else
            {
                _logger.LogError("Load Prouducts failed");
                return Json(new { message = "Load Prouducts failed" });
            }

        }

        [HttpPost]
        public async Task  Create([FromBody] Product product)
        {
            if(product != null)
            {
                await _productService.InsertProduct(product);
                _logger.LogInformation("Create Prouduct successfully");

            }
            else
            {
                _logger.LogError("Create Prouduct failed");
            }
           
        }

        [HttpPost]
        public async Task  Update([FromBody] Product product)
        {
            if(product !=null)
            {
                await _productService.UpdateProduct(product);
                _logger.LogInformation("Update Prouduct");
            }
            else
            {
                 _logger.LogError("Update Prouduct failed");
            }

        }

        [HttpPost]
        public async Task  Delete([FromBody] int productId)
        {
            if(productId >0)
            {
                await _productService.DeleteProduct(productId);
                _logger.LogInformation("Delete Prouduct");
            }
            else
            {
                _logger.LogError("Delete Prouduct failed");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}