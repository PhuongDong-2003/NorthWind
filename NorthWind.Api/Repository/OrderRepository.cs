using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Repository
{
    public class OrderRepository : IOrderRepository
    {


        private readonly IConfiguration _configuration;

        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public Order GetOrderByID(int orderID)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "SELECT * FROM Orders WHERE OrderID = @OrderID and Status = 0";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@OrderID", orderID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Order oder = new Order
                        {
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            CustomerID = reader.IsDBNull(reader.GetOrdinal("OrderDate")) ? null : reader.GetString(reader.GetOrdinal("CustomerID")),
                            EmployeeID = reader.IsDBNull(reader.GetOrdinal("EmployeeID")) ? null : reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                            OrderDate = reader.IsDBNull(reader.GetOrdinal("OrderDate")) ? null : reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                            RequiredDate = reader.IsDBNull(reader.GetOrdinal("RequiredDate")) ? null : reader.GetDateTime(reader.GetOrdinal("RequiredDate")),
                            ShippedDate = reader.IsDBNull(reader.GetOrdinal("ShippedDate")) ? null : reader.GetDateTime(reader.GetOrdinal("ShippedDate")),
                            ShipVia = reader.IsDBNull(reader.GetOrdinal("ShipVia")) ? null : reader.GetInt32(reader.GetOrdinal("ShipVia")),
                            Freight = reader.IsDBNull(reader.GetOrdinal("Freight")) ? null : reader.GetDecimal(reader.GetOrdinal("Freight")),
                            ShipName = reader.IsDBNull(reader.GetOrdinal("ShipName")) ? null : reader.GetString(reader.GetOrdinal("ShipName")),
                            ShipAddress = reader.IsDBNull(reader.GetOrdinal("ShipAddress")) ? null : reader.GetString(reader.GetOrdinal("ShipAddress")),
                            ShipCity = reader.IsDBNull(reader.GetOrdinal("ShipCity")) ? null : reader.GetString(reader.GetOrdinal("ShipCity")),
                            ShipRegion = reader.IsDBNull(reader.GetOrdinal("ShipRegion")) ? null : reader.GetString(reader.GetOrdinal("ShipRegion")),
                            ShipPostalCode = reader.IsDBNull(reader.GetOrdinal("ShipPostalCode")) ? null : reader.GetString(reader.GetOrdinal("ShipPostalCode")),
                            ShipCountry = reader.IsDBNull(reader.GetOrdinal("ShipCountry")) ? null : reader.GetString(reader.GetOrdinal("ShipCountry")),
                           
                        };
                        return oder;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public IEnumerable<Order> GetOrder()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "SELECT * FROM Orders where Status=0";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var resultList = new List<Order>();
                    while (reader.Read())
                    {
                        Order oder = new Order

                        {
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            CustomerID = reader.IsDBNull(reader.GetOrdinal("CustomerID")) ? null : reader.GetString(reader.GetOrdinal("CustomerID")),
                            EmployeeID = reader.IsDBNull(reader.GetOrdinal("EmployeeID")) ? null : reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                            OrderDate = reader.IsDBNull(reader.GetOrdinal("OrderDate")) ? null : reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                            RequiredDate = reader.IsDBNull(reader.GetOrdinal("RequiredDate")) ? null : reader.GetDateTime(reader.GetOrdinal("RequiredDate")),
                            ShippedDate = reader.IsDBNull(reader.GetOrdinal("ShippedDate")) ? null : reader.GetDateTime(reader.GetOrdinal("ShippedDate")),
                            ShipVia = reader.IsDBNull(reader.GetOrdinal("ShipVia")) ? null : reader.GetInt32(reader.GetOrdinal("ShipVia")),
                            Freight = reader.IsDBNull(reader.GetOrdinal("Freight")) ? null : reader.GetDecimal(reader.GetOrdinal("Freight")),
                            ShipName = reader.IsDBNull(reader.GetOrdinal("ShipName")) ? null : reader.GetString(reader.GetOrdinal("ShipName")),
                            ShipAddress = reader.IsDBNull(reader.GetOrdinal("ShipAddress")) ? null : reader.GetString(reader.GetOrdinal("ShipAddress")),
                            ShipCity = reader.IsDBNull(reader.GetOrdinal("ShipCity")) ? null : reader.GetString(reader.GetOrdinal("ShipCity")),
                            ShipRegion = reader.IsDBNull(reader.GetOrdinal("ShipRegion")) ? null : reader.GetString(reader.GetOrdinal("ShipRegion")),
                            ShipPostalCode = reader.IsDBNull(reader.GetOrdinal("ShipPostalCode")) ? null : reader.GetString(reader.GetOrdinal("ShipPostalCode")),
                            ShipCountry = reader.IsDBNull(reader.GetOrdinal("ShipCountry")) ? null : reader.GetString(reader.GetOrdinal("ShipCountry")),
                            
                        };
                        resultList.Add(oder);
                    }

                    return resultList;
                }
            }
        }

        public void InsertOrder(Order order)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = @"INSERT INTO Orders ( CustomerID, EmployeeID, OrderDate, RequiredDate, 
                               ShippedDate, ShipVia,  Freight, ShipName,  ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry, Status)
                               VALUES (@CustomerID, @EmployeeID, @OrderDate, @RequiredDate, @ShippedDate,  @ShipVia, @Freight, 
                              @ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry, @Status)";

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            command.Parameters.AddWithValue("@CustomerID", order.CustomerID ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@EmployeeID", order.EmployeeID ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@OrderDate", order.OrderDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@RequiredDate", order.RequiredDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShippedDate", order.ShippedDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipVia", order.ShipVia ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Freight", order.Freight ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipName", order.ShipName ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipAddress", order.ShipAddress ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipCity", order.ShipCity ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipRegion", order.ShipRegion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipPostalCode", order.ShipCountry ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipCountry", order.ShipCountry ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Status", false);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Order đã được thêm thành công.");
            }
            else
            {
                Console.WriteLine("Không thể thêm Order.");
            }
        }

        public void UpdateOrder(Order order)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            SqlConnection connection = new SqlConnection(connectionString);
            {
                connection.Open();
                string sqlQuery = @"UPDATE Orders
                                Set CustomerID = @CustomerID, 
                                EmployeeID = @EmployeeID ,
                                OrderDate = @OrderDate,
                                RequiredDate = @RequiredDate, 
                                ShippedDate = @ShippedDate,
                                ShipVia = @ShipVia,  
                                Freight = @Freight,
                                ShipName = @ShipName, 
                                ShipAddress = @ShipAddress, 
                                ShipCity = @ShipCity,
                                ShipRegion = @ShipRegion,
                                ShipPostalCode = @ShipPostalCode,
                                ShipCountry = @ShipCountry,
                                Status = @Status
                                WHERE OrderID = @OrderID";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@OrderID", order.OrderID);
                command.Parameters.AddWithValue("@CustomerID", order.CustomerID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EmployeeID", order.EmployeeID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@OrderDate", order.OrderDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@RequiredDate", order.RequiredDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ShippedDate", order.ShippedDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ShipVia", order.ShipVia ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Freight", order.Freight ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ShipName", order.ShipName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ShipAddress", order.ShipAddress ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ShipCity", order.ShipCity ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ShipRegion", order.ShipRegion ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ShipPostalCode", order.ShipCountry ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ShipCountry", order.ShipCountry ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Status", false);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Order đã được cập nhật thành công.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy Customer tương ứng.");
                    return;
                }

            }
        }

        public void DeleteOrder(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "UPDATE Orders SET Status = 1 WHERE OrderID = @OrderID";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {

                command.Parameters.AddWithValue("@OrderID", id);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Order đã được xóa thành công.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy  có OrderID tương ứng.");
                }
            }
        }

        public IEnumerable<Order> GetOrderPaged(int page, int pageSize)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            var resultList = new List<Order>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = @"
                            Select *
                            from (
                                    SELECT *, ROW_NUMBER() OVER ( ORDER BY OrderID) row_num,
                                    Count(1) OVER () AS TotalRow  
                                    FROM Orders where Status = 0
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
                            Order oder = new Order

                            {
                                OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                                CustomerID = reader.IsDBNull(reader.GetOrdinal("CustomerID")) ? null : reader.GetString(reader.GetOrdinal("CustomerID")),
                                EmployeeID = reader.IsDBNull(reader.GetOrdinal("EmployeeID")) ? null : reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                                OrderDate = reader.IsDBNull(reader.GetOrdinal("OrderDate")) ? null : reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                                RequiredDate = reader.IsDBNull(reader.GetOrdinal("RequiredDate")) ? null : reader.GetDateTime(reader.GetOrdinal("RequiredDate")),
                                ShippedDate = reader.IsDBNull(reader.GetOrdinal("ShippedDate")) ? null : reader.GetDateTime(reader.GetOrdinal("ShippedDate")),
                                ShipVia = reader.IsDBNull(reader.GetOrdinal("ShipVia")) ? null : reader.GetInt32(reader.GetOrdinal("ShipVia")),
                                Freight = reader.IsDBNull(reader.GetOrdinal("Freight")) ? null : reader.GetDecimal(reader.GetOrdinal("Freight")),
                                ShipName = reader.IsDBNull(reader.GetOrdinal("ShipName")) ? null : reader.GetString(reader.GetOrdinal("ShipName")),
                                ShipAddress = reader.IsDBNull(reader.GetOrdinal("ShipAddress")) ? null : reader.GetString(reader.GetOrdinal("ShipAddress")),
                                ShipCity = reader.IsDBNull(reader.GetOrdinal("ShipCity")) ? null : reader.GetString(reader.GetOrdinal("ShipCity")),
                                ShipRegion = reader.IsDBNull(reader.GetOrdinal("ShipRegion")) ? null : reader.GetString(reader.GetOrdinal("ShipRegion")),
                                ShipPostalCode = reader.IsDBNull(reader.GetOrdinal("ShipPostalCode")) ? null : reader.GetString(reader.GetOrdinal("ShipPostalCode")),
                                ShipCountry = reader.IsDBNull(reader.GetOrdinal("ShipCountry")) ? null : reader.GetString(reader.GetOrdinal("ShipCountry")),                              
                                Page = page,
                                PageSize = pageSize,
                                RowNum = reader.GetInt64(reader.GetOrdinal("row_num")),
                                TotalRow = reader.GetInt32(reader.GetOrdinal("TotalRow")),
                                TotalPages = (int)Math.Ceiling((double)reader.GetInt32(reader.GetOrdinal("TotalRow")) / pageSize)
                            };
                            resultList.Add(oder);
                        }
                    }
                }

                return resultList;

            }
        }

        public void InsertOrderAccount(Order order)
        {

            DateTime currentTime = DateTime.Now;
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = @"INSERT INTO Orders ( CustomerID, EmployeeID, OrderDate, RequiredDate, 
                               ShippedDate, ShipVia,  Freight, ShipName,  ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry, Email,  Status)
                               VALUES (@CustomerID, @EmployeeID, @OrderDate, @RequiredDate, @ShippedDate,  @ShipVia, @Freight, 
                              @ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry, @Status)";

            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@OrderID", order.OrderID);
            command.Parameters.AddWithValue("@CustomerID", order.CustomerID ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@CustomerID", order.CustomerID ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@EmployeeID", order.EmployeeID ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@OrderDate", order.OrderDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@RequiredDate", order.RequiredDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShippedDate", order.ShippedDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipVia", order.ShipVia ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Freight", order.Freight ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipName", order.ShipName ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipAddress", order.ShipAddress ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipCity", order.ShipCity ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipRegion", order.ShipRegion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipPostalCode", order.ShipCountry ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ShipCountry", order.ShipCountry ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Status", false);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Order đã được thêm thành công.");
            }
            else
            {
                Console.WriteLine("Không thể thêm Order.");
            }
        }

        public IEnumerable<Order> GetOrderByCustomerID(string CustomerID)
        {
            var resultList = new List<Order>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "SELECT * FROM Orders WHERE CustomerID like @CustomerID and Status = 0";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@CustomerID", CustomerID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order oder = new Order
                        {
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            CustomerID = reader.IsDBNull(reader.GetOrdinal("CustomerID")) ? null : reader.GetString(reader.GetOrdinal("CustomerID")),
                            EmployeeID = reader.IsDBNull(reader.GetOrdinal("EmployeeID")) ? null : reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                            OrderDate = reader.IsDBNull(reader.GetOrdinal("OrderDate")) ? null : reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                            RequiredDate = reader.IsDBNull(reader.GetOrdinal("RequiredDate")) ? null : reader.GetDateTime(reader.GetOrdinal("RequiredDate")),
                            ShippedDate = reader.IsDBNull(reader.GetOrdinal("ShippedDate")) ? null : reader.GetDateTime(reader.GetOrdinal("ShippedDate")),
                            ShipVia = reader.IsDBNull(reader.GetOrdinal("ShipVia")) ? null : reader.GetInt32(reader.GetOrdinal("ShipVia")),
                            Freight = reader.IsDBNull(reader.GetOrdinal("Freight")) ? null : reader.GetDecimal(reader.GetOrdinal("Freight")),
                            ShipName = reader.IsDBNull(reader.GetOrdinal("ShipName")) ? null : reader.GetString(reader.GetOrdinal("ShipName")),
                            ShipAddress = reader.IsDBNull(reader.GetOrdinal("ShipAddress")) ? null : reader.GetString(reader.GetOrdinal("ShipAddress")),
                            ShipCity = reader.IsDBNull(reader.GetOrdinal("ShipCity")) ? null : reader.GetString(reader.GetOrdinal("ShipCity")),
                            ShipRegion = reader.IsDBNull(reader.GetOrdinal("ShipRegion")) ? null : reader.GetString(reader.GetOrdinal("ShipRegion")),
                            ShipPostalCode = reader.IsDBNull(reader.GetOrdinal("ShipPostalCode")) ? null : reader.GetString(reader.GetOrdinal("ShipPostalCode")),
                            ShipCountry = reader.IsDBNull(reader.GetOrdinal("ShipCountry")) ? null : reader.GetString(reader.GetOrdinal("ShipCountry")),

                        };
                        resultList.Add(oder);
                    }
                }
            }
            return resultList;

        }
    }
}

