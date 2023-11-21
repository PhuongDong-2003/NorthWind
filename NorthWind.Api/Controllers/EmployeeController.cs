using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NorthWind.Core.Entity;
using NorthWind.Api.Repository;
using Microsoft.AspNetCore.Authorization;

namespace NorthWind.Api.Controllers
{
    [Authorize(Roles = "web")]
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
        public IActionResult GetEmployee(int page,  int pageSize)

        {

            try
            {

                var employees = _employeeRepository.GetEmployeePaged(page, pageSize);

                if (employees != null)
                {

                    return Ok(employees);
                }

                return NotFound();
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

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
        public IActionResult Create(Employee employee)
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
        public IActionResult Update(Employee employee)
        {
            try
            {
                _employeeRepository.UpdateEmployee(employee);
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