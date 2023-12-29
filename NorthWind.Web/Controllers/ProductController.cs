using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWind.Core.Entity;
using NorthWind.Web.Service;

namespace NorthWind.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Product(int page = 1, int pageSize = 5)
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

        public async Task<IActionResult> Edit(int ProductID)
        {

            var product = await _productService.GetProductByID(ProductID);
            ViewData["product"] = product;
            int page = 1;
            int pageSize = 5;
            await Product(page, pageSize);
            return View("Product");

        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {

            try
            {
                await _productService.InsertProduct(product);
                int page = 1;
                int pageSize = 5;
                await Product(page, pageSize);

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message} ");

            }

            return View("Product");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {

            try
            {
                await _productService.UpdateProduct(product);


            }

            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message} ");

            }
            await Product();

            return View("Product", product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int ProductID)
        {

            if (ProductID > 0)
            {
                await _productService.DeleteProduct(ProductID);
                return RedirectToAction("Product");

            }
            else
            {
                Console.WriteLine("Delete fail");
            }
            return View("Product");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}