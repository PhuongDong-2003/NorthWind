using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Api.Repository;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Controllers
{   [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
         private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

         [HttpGet()]
        public IActionResult GetAll()

        {
            try
            {
                var customer = _customerRepository.GetCustomer();
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetById(string  id)
        {

            var customer = _customerRepository.GetCustomerByID(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

                [HttpPost]
        public IActionResult Create(Customer customer)
        {

            try
            {
                _customerRepository.InsertCustomer(customer);
                return Ok("Thêm thành công");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut]
        public IActionResult Update(Customer customer)
        {
            try
            {
                _customerRepository.UpdateCustomer(customer);
               
                return Ok("Customer đã được cập nhật thành công.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

         [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _customerRepository.DeleteCustomer(id);
                return Ok("Customer đã được xóa thành công.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("p")]
        public IActionResult GetCustomer(int page,  int pageSize)

        {

            try
            {

                var customer = _customerRepository.GetCustomerPaged(page, pageSize);

                if (customer != null)
                {

                    return Ok(customer);
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