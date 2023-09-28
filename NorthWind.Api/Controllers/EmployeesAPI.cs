using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesAPI : ControllerBase     
    {

    [HttpGet]
    public IActionResult GetAll()
    {
        string connectionString = "Server=LAPTOP-L2EEIEB9\\SQLEXPRESS;Database=NorthWind;User=sa;Password=123;Trusted_Connection=true; TrustServerCertificate = true; Encrypt=False";
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
                {
                connection.Open();
                string sqlQuery = "SELECT * FROM Employees";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var resultList = new List<Employee>();
                        while (reader.Read())
                        {
                            Employee employee = new Employee
                            {
                                EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                Title = reader.IsDBNull(reader.GetOrdinal("Title")) ? null : reader.GetString(reader.GetOrdinal("Title")),
                                TitleOfCourtesy = reader.IsDBNull(reader.GetOrdinal("TitleOfCourtesy")) ? null : reader.GetString(reader.GetOrdinal("TitleOfCourtesy")),
                                BirthDate = reader.IsDBNull(reader.GetOrdinal("BirthDate")) ? null : reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                                HireDate = reader.IsDBNull(reader.GetOrdinal("HireDate")) ? null : reader.GetDateTime(reader.GetOrdinal("HireDate")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader.GetString(reader.GetOrdinal("City")),
                                Region = reader.IsDBNull(reader.GetOrdinal("Region")) ? null : reader.GetString(reader.GetOrdinal("Region")),
                                PostalCode = reader.IsDBNull(reader.GetOrdinal("PostalCode")) ? null : reader.GetString(reader.GetOrdinal("PostalCode")),
                                Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? null : reader.GetString(reader.GetOrdinal("Country")),
                                HomePhone = reader.IsDBNull(reader.GetOrdinal("HomePhone")) ? null : reader.GetString(reader.GetOrdinal("HomePhone")),
                                Extension = reader.IsDBNull(reader.GetOrdinal("Extension")) ? null : reader.GetString(reader.GetOrdinal("Extension")),
                                Photo = reader.IsDBNull(reader.GetOrdinal("Photo")) ? null : (byte[])reader["Photo"],
                                Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                                ReportsTo = reader.IsDBNull(reader.GetOrdinal("ReportsTo")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("ReportsTo")),
                                PhotoPath = reader.IsDBNull(reader.GetOrdinal("PhotoPath")) ? null : reader.GetString(reader.GetOrdinal("PhotoPath"))
                            };
                            resultList.Add(employee);
                        }                           

                        return Ok(resultList);
                    }
                }
            }
                    
        }
        catch (Exception ex)
            {
            return BadRequest(ex.Message);
            }
        
    }
    }
}