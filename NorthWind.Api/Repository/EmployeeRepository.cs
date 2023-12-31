using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NorthWind.Api.Controllers;
using NorthWind.Core.Entity;


namespace NorthWind.Api.Repository
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionStrings databaseSetting;
        private readonly DatabaseContext _context;

        public EmployeeRepository(IConfiguration configuration, ConnectionStrings databaseSetting, DatabaseContext context)
        {
            _configuration = configuration;
            this.databaseSetting = databaseSetting;
            _context = context;
        }

        public void DeleteEmployee(int id)
        {

            string connectionString = _configuration.GetConnectionString("Connection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {

                command.Parameters.AddWithValue("@EmployeeId", id);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Employee đã được xóa thành công.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy nhân viên có EmployeeId tương ứng.");
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
          
        }

        public Employee GetEmployeeByID(int employeeId)
        {
            string connectionString = _configuration.GetConnectionString("Connection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
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
                        return employee;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            //  var employees = _context.Employees.Where(x => x.EmployeeId == employeeId).Include(x => x.Orders).FirstOrDefault();
                           
            // if (employees is not null)
            // {

            //     return employees;
            // }

            // return null;
        }

        public IEnumerable<Employee> GetEmployee()
        {
            string connectionString = _configuration.GetConnectionString("Connection");
            SqlConnection connection = new SqlConnection(connectionString);
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

                    return resultList;
                }
            }
        }

        public void InsertEmployee(Employee employee)
        {
            string connectionString = _configuration.GetConnectionString("Connection");
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = @"INSERT INTO Employees (LastName, FirstName, Title, TitleOfCourtesy, 
                               BirthDate, HireDate,  Address, City, Region, PostalCode, 
                               Country, HomePhone, Extension, Photo, Notes, ReportsTo, PhotoPath)
                               VALUES (@LastName, @FirstName, @Title, @TitleOfCourtesy, @BirthDate, 
                               @HireDate, @Address, @City, @Region, @PostalCode, @Country, @HomePhone, 
                               @Extension, @Photo, @Notes, @ReportsTo, @PhotoPath)";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@LastName", employee.LastName);
            command.Parameters.AddWithValue("@FirstName", employee.FirstName);
            command.Parameters.AddWithValue("@Title", employee.Title ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@TitleOfCourtesy", employee.TitleOfCourtesy ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@BirthDate", employee.BirthDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@HireDate", employee.HireDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Address", employee.Address ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@City", employee.City ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Region", employee.Region ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@PostalCode", employee.PostalCode ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Country", employee.Country ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@HomePhone", employee.HomePhone ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Extension", employee.Extension ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Photo", employee.Photo ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Notes", employee.Notes ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ReportsTo", employee.ReportsTo ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@PhotoPath", employee.PhotoPath ?? (object)DBNull.Value);
            // if (employee is not null)
            // {
            //     _context.Employees.Add(employee);
            //     int result = _context.SaveChanges();

            //     if (result > 0)
            //     {
            //         Console.WriteLine("Employee đã được thêm thành công.");
            //     }
            //     else
            //     {
            //         Console.WriteLine("Không thể thêm Employee.");
            //     }

            // }

        }

        public void UpdateEmployee(Employee employee)
        {
            string connectionString = _configuration.GetConnectionString("Connection");

            SqlConnection connection = new SqlConnection(connectionString);
            {
                connection.Open();
                string sqlQuery = @"UPDATE Employees
                                 SET LastName = @LastName,
                                     FirstName = @FirstName,
                                     Title = @Title,
                                     TitleOfCourtesy = @TitleOfCourtesy,
                                     BirthDate = @BirthDate,
                                     HireDate = @HireDate,
                                     Address = @Address,
                                     City = @City,
                                     Region = @Region,
                                     PostalCode = @PostalCode,
                                     Country = @Country,
                                     HomePhone = @HomePhone,
                                     Extension = @Extension,
                                     Photo = @Photo,
                                     Notes = @Notes,
                                     ReportsTo = @ReportsTo,
                                     PhotoPath = @PhotoPath
                                 WHERE EmployeeId = @EmployeeId";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@Title", employee.Title ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TitleOfCourtesy", employee.TitleOfCourtesy ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@BirthDate", employee.BirthDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@HireDate", employee.HireDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Address", employee.Address ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@City", employee.City ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Region", employee.Region ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@PostalCode", employee.PostalCode ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Country", employee.Country ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@HomePhone", employee.HomePhone ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Extension", employee.Extension ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Photo", employee.Photo ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Notes", employee.Notes ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ReportsTo", employee.ReportsTo ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@PhotoPath", employee.PhotoPath ?? (object)DBNull.Value);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Employee đã được cập nhật thành công.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy nhân viên có EmployeeId tương ứng.");
                    return;
                }

            }

        }

        public IEnumerable<Employee> GetEmployeePaged(int page, int pageSize)
        {
            string connectionString = _configuration.GetConnectionString("Connection");
            var resultList = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = @"
                            Select *
                            from (
                                SELECT *, ROW_NUMBER() OVER ( ORDER BY EmployeeID) row_num,
                                    Count(1) OVER () AS TotalRow  
                                FROM Employees
                            ) s
                            where s.row_num between (@page-1)*@pageSize+1 and @pageSize*@page;                                          
                ";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@page", page);
                    command.Parameters.AddWithValue("@pageSize", pageSize);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
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
                                PhotoPath = reader.IsDBNull(reader.GetOrdinal("PhotoPath")) ? null : reader.GetString(reader.GetOrdinal("PhotoPath")),
                                Page = page,
                                PageSize = pageSize,
                                RowNum = reader.GetInt64(reader.GetOrdinal("row_num")),
                                TotalRow = reader.GetInt32(reader.GetOrdinal("TotalRow")),
                                TotalPages = (int)Math.Ceiling((double)reader.GetInt32(reader.GetOrdinal("TotalRow")) / pageSize)

                            };
                            resultList.Add(employee);
                        }
                    }
                }

                return resultList;

            }

            //  var totalRow = _context.Employees.Count(); 
            // var employees = _context.Employees
            //     .Skip((page - 1) * pageSize)
            //     .Take(pageSize)
            //     .ToList();

            // foreach (var employee in employees)
            // {
            //     employee.TotalRow = totalRow;
            //     employee.Page = page;
            //     employee.PageSize = pageSize;
            //     employee.CalculatePagination(pageSize);
            // }

            // return employees;

        }
    }

}



