using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Customer> GetCustomer()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "SELECT * FROM Customers where Status=0";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var resultList = new List<Customer>();
                    while (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            CustomerID = reader.GetString(reader.GetOrdinal("CustomerID")),
                            CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                            ContactName = reader.IsDBNull(reader.GetOrdinal("ContactName")) ? null : reader.GetString(reader.GetOrdinal("ContactName")),
                            ContactTitle = reader.IsDBNull(reader.GetOrdinal("ContactTitle")) ? null : reader.GetString(reader.GetOrdinal("ContactTitle")),
                            Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                            City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader.GetString(reader.GetOrdinal("City")),
                            Region = reader.IsDBNull(reader.GetOrdinal("Region")) ? null : reader.GetString(reader.GetOrdinal("Region")),
                            PostalCode = reader.IsDBNull(reader.GetOrdinal("PostalCode")) ? null : reader.GetString(reader.GetOrdinal("PostalCode")),
                            Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? null : reader.GetString(reader.GetOrdinal("Country")),
                            Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                            Fax = reader.IsDBNull(reader.GetOrdinal("Fax")) ? null : reader.GetString(reader.GetOrdinal("Fax")),
                            Status = reader.GetBoolean(reader.GetOrdinal("Status"))
                        };
                        resultList.Add(customer);
                    }

                    return resultList;
                }


            }
        }

        public Customer GetCustomerByID(string customerId)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "SELECT * FROM Customers WHERE CustomerID = @CustomerID and Status = 0";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@CustomerID", customerId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Customer customer = new Customer
                        {
                            CustomerID = reader.GetString(reader.GetOrdinal("CustomerID")),
                            CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                            ContactName = reader.IsDBNull(reader.GetOrdinal("ContactName")) ? null : reader.GetString(reader.GetOrdinal("ContactName")),
                            ContactTitle = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("ContactTitle")),
                            Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                            City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader.GetString(reader.GetOrdinal("City")),
                            Region = reader.IsDBNull(reader.GetOrdinal("Region")) ? null : reader.GetString(reader.GetOrdinal("Region")),
                            PostalCode = reader.IsDBNull(reader.GetOrdinal("PostalCode")) ? null : reader.GetString(reader.GetOrdinal("PostalCode")),
                            Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? null : reader.GetString(reader.GetOrdinal("Country")),
                            Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                            Fax = reader.IsDBNull(reader.GetOrdinal("Fax")) ? null : reader.GetString(reader.GetOrdinal("Fax")),
                            Status = reader.GetBoolean(reader.GetOrdinal("Status")),
                        };
                        return customer;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public void InsertCustomer(Customer customer)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = @"INSERT INTO Customers (CustomerID, CompanyName, ContactName, ContactTitle, Address, 
                               City, Region,  PostalCode, Country,  Phone, Fax, Status)
                               VALUES (@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, 
                               @Region, @PostalCode, @Country, @Phone, @Fax, @Status)";

            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
            command.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
            command.Parameters.AddWithValue("@ContactName", customer.ContactTitle ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ContactTitle", customer.ContactTitle ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Address", customer.Address ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@City", customer.City ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Region", customer.Region ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@PostalCode", customer.PostalCode ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Country", customer.Country ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Phone", customer.Phone ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Fax", customer.Fax ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Status", false);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Customer đã được thêm thành công.");
            }
            else
            {
                Console.WriteLine("Không thể thêm Employee.");
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            SqlConnection connection = new SqlConnection(connectionString);
            {
                connection.Open();
                string sqlQuery = @"UPDATE Customers
                                SET CustomerID = @CustomerID,
                                CompanyName = @CompanyName, 
                                ContactTitle = @ContactTitle ,
                                ContactName = @ContactName,
                                Address = @Address, 
                                City = @City,
                                Region = @Region,  
                                PostalCode = @PostalCode,
                                Country = @Country, 
                                Phone = @Phone, 
                                Fax = @Fax
                                Status = @Status
                                WHERE CustomerID = @CustomerID";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                command.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
                command.Parameters.AddWithValue("@ContactTitle", customer.ContactTitle ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Address", customer.Address ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@City", customer.City ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Region", customer.Region ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@PostalCode", customer.PostalCode ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Country", customer.Country ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Phone", customer.Phone ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Fax", customer.Fax ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Status", true);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Customer đã được cập nhật thành công.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy nhân viên có Customer tương ứng.");
                    return;
                }

            }
        }

        public void DeleteCustomer(string id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "UPDATE Customers SET Status = 1 WHERE CustomerID = @CustomerID";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {

                command.Parameters.AddWithValue("@CustomerID", id);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Customer đã được xóa thành công.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy nhân viên có CustomerID tương ứng.");
                }
            }
        }

        public IEnumerable<Customer> GetCustomerPaged(int page, int pageSize)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            var resultList = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = @"
                            Select *
                            from (
                                SELECT *, ROW_NUMBER() OVER ( ORDER BY CustomerID) row_num,
                                Count(1) OVER () AS TotalRow  
                                FROM Customers where Status = 0
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
                            Customer customer = new Customer
                            {
                                CustomerID = reader.GetString(reader.GetOrdinal("CustomerID")),
                                CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                                ContactName = reader.IsDBNull(reader.GetOrdinal("ContactName")) ? null : reader.GetString(reader.GetOrdinal("ContactName")),
                                ContactTitle = reader.IsDBNull(reader.GetOrdinal("ContactTitle")) ? null : reader.GetString(reader.GetOrdinal("ContactTitle")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader.GetString(reader.GetOrdinal("City")),
                                Region = reader.IsDBNull(reader.GetOrdinal("Region")) ? null : reader.GetString(reader.GetOrdinal("Region")),
                                PostalCode = reader.IsDBNull(reader.GetOrdinal("PostalCode")) ? null : reader.GetString(reader.GetOrdinal("PostalCode")),
                                Country = reader.IsDBNull(reader.GetOrdinal("Country")) ? null : reader.GetString(reader.GetOrdinal("Country")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                                Fax = reader.IsDBNull(reader.GetOrdinal("Fax")) ? null : reader.GetString(reader.GetOrdinal("Fax")),
                                Page = page,
                                PageSize = pageSize,
                                RowNum = reader.GetInt64(reader.GetOrdinal("row_num")),
                                TotalRow = reader.GetInt32(reader.GetOrdinal("TotalRow")),
                                TotalPages = (int)Math.Ceiling((double)reader.GetInt32(reader.GetOrdinal("TotalRow")) / pageSize)
                            };
                            resultList.Add(customer);
                        }
                    }
                }

                return resultList;

            }
        }
    }
}

