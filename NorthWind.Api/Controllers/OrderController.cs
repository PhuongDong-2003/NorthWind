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
                var orders = _orderRepository.GetOrder();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            var order = _orderRepository.GetOrderByID(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("CustomerID")]
        public IActionResult GetByCusomer(string CustomerID)
        {

            var orderDetail = _orderRepository.GetOrderByCustomerID(CustomerID);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return Ok(orderDetail);
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
        [HttpPost("OdAccount")]
        public IActionResult CreateOdACount(Order order)
        {

            try
            {
                _orderRepository.InsertOrderAccount(order);
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

                var orders = _orderRepository.GetOrderPaged(page, pageSize);

                if (orders != null)
                {

                    return Ok(orders);
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