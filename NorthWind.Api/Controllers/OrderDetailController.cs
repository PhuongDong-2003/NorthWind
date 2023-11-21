using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Api.Repository;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Controllers
{
    [Authorize(Roles = "web")]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        [HttpGet()]
        public IActionResult GetAll()

        {
            try
            {
                var orderDetails = _orderDetailRepository.GetOrderDetails();
                return Ok(orderDetails);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("id")]
        public IActionResult GetById(int orderID)
        {

            var orderDetail = _orderDetailRepository.GetOrderDetailsByID(orderID);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return Ok(orderDetail);
        }

        
  


        [HttpPost]
        public IActionResult Create(OrderDetail orderDetail)
        {

            try
            {
                _orderDetailRepository.InsertOrderDetail(orderDetail);
                return Ok("Thêm thành công");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut]
        public IActionResult Update(OrderDetail orderDetail)
        {
            try
            {
                _orderDetailRepository.UpdateOrderDetail(orderDetail);
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
                _orderDetailRepository.DeleteOrdeDetail(id);
                return Ok("Xóa thành công.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}







