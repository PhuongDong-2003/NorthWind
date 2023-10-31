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
    public class OrderController : ControllerBase
    {
        
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet()]
        public IActionResult GetAll()

        {
            try
            {
                var employee = _orderRepository.GetOrder();
                return Ok(employee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            var product = _orderRepository.GetOrderByID(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {

            try
            {
                _orderRepository.InsertOrder(order);
                return Ok("Thêm thành công");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut]
        public IActionResult Update(Order order)
        {
            try
            {
                _orderRepository.UpdateOrder(order);

                return Ok("Cập nhật thành công.");
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
               _orderRepository.DeleteOrder(id);
                return Ok("Xóa thành công.");
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

                var products = _orderRepository.GetOrderPaged(page, pageSize);

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
        // [HttpGet("find")]
        // public IActionResult Find(int OrderID)

        // {

        //     try
        //     {

        //         var order = _orderRepository.Find(OrderID);

        //         if (order != null)
        //         {

        //             return Ok(order);
        //         }

        //         return NotFound();
        //     }
        //     catch (Exception ex)
        //     {

        //         return StatusCode(500, $"Internal Server Error: {ex.Message}");
        //     }

        // }
    }
}