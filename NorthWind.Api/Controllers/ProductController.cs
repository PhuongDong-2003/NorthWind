using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Api.Repository;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {


        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet()]
        public IActionResult GetAll()

        {
            try
            {
                var products = _productRepository.GetProduct();
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            var product = _productRepository.GetProductByID(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

         [HttpGet("Name")]
        public IActionResult GetByName(string ProductName)
        {

            var product = _productRepository.GetProductByName(ProductName);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        } 

        [HttpPost]
        public IActionResult Create(Product product)
        {

            try
            {
                _productRepository.InsertProduct(product);
                return Ok("Thêm thành công");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut]
        public IActionResult Update(Product product)
        {
            try
            {
                _productRepository.UpdateProduct(product);

                return Ok("Product đã được cập nhật thành công.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productRepository.DeleteProduct(id);
                return Ok("Product đã được xóa thành công.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("p")]
        public IActionResult GetProductPage(int page, int pageSize)

        {

            try
            {

                var products = _productRepository.GetProductPaged(page, pageSize);

                if (products != null)
                {

                    return Ok(products);
                }

                return NotFound();
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

    }
}