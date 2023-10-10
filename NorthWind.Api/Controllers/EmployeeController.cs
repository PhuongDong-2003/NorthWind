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

    public EmployeeController(EmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
 
    [HttpGet]
    public IActionResult GetAll()
    
    {
        try
        {
                 _employeeRepository.GetStudents();
                 return Ok(_employeeRepository);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
      

        
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        
        var employee = _employeeRepository.GetStudentByID(id);
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
                 _employeeRepository.InsertStudent(employee);
                 return Ok(_employeeRepository);
        }
            catch(Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpPut("{id}")]
    public IActionResult Update(  Employee employee)
    {
        try
            {
                    _employeeRepository.UpdateStudent(employee);
                    return Ok(_employeeRepository);
            }
                catch(Exception e)
            {
                return BadRequest(e.Message);
            }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
                try
            {
                    _employeeRepository.DeleteStudent(id);
                    return Ok(_employeeRepository);
            }
                catch(Exception e)
            {
                return BadRequest(e.Message);
            }
    }

    }
}