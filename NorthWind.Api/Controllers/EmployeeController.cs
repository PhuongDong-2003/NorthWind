using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NorthWind.Core.Entity;
using NorthWind.Api.Repository;

namespace NorthWind.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet()]
        public IActionResult GetAll()

        {
            try
            {
                var employee = _employeeRepository.GetEmployee();
                return Ok(employee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }



        }

         [HttpGet("p")]
        public IActionResult GetEmployee(int page = 1, int pageSize = 10)

        {
           var employee = _employeeRepository.GetEmployee();

            int totalItems = employee.Count();

        var productsOnPage = employee
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        var result = new
        {
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Data = productsOnPage
        };

        return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            var employee = _employeeRepository.GetEmployeeByID(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Create( Employee employee)
        {

            try
            {
                _employeeRepository.InsertEmployee(employee);
                return Ok("Thêm thành công");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update( Employee employee)
        {
            try
            {
                _employeeRepository.UpdateEmployee( employee);
                return Ok("Employee đã được cập nhật thành công.");
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
                _employeeRepository.DeleteEmployee(id);
                return Ok("Employee đã được xóa thành công.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}