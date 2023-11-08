using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {

        private readonly IConfiguration _configuration;

        public OrderDetailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IEnumerable<OrderDetail> GetOrderDetails()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "select* from OrderDetails ";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var resultList = new List<OrderDetail>();
                    while (reader.Read())
                    {
                        OrderDetail oder = new OrderDetail

                        {
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                            Quantity = reader.GetInt16(reader.GetOrdinal("Quantity")),
                            Discount = reader.GetFloat(reader.GetOrdinal("Discount"))

                        };
                        resultList.Add(oder);
                    }

                    return resultList;
                }
            }
        }
        public OrderDetail GetOrderDetailsByID(int orderID)
        {

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "select* from  OrderDetails";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@OrderID", orderID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        OrderDetail orderDetail = new OrderDetail
                        {
                            OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                            Quantity = reader.GetInt16(reader.GetOrdinal("Quantity")),
                            Discount = reader.GetFloat(reader.GetOrdinal("Discount"))
                        };
                        return orderDetail;
                    }
                    else
                    {
                        return null;
                    }
                }
            }


        }

        public void InsertOrderDetail(OrderDetail orderDetail)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string sqlQuery = @"INSERT INTO OrderDetails (OrderID, ProductID, UnitPrice, Quantity, Discount)
                         
                               VALUES (@OrderID, @ProductID, @UnitPrice, @Quantity, @Discount)";

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            command.Parameters.AddWithValue("@OrderID", orderDetail.OrderID);
            command.Parameters.AddWithValue("@ProductID", orderDetail.ProductID);
            command.Parameters.AddWithValue("@UnitPrice", orderDetail.UnitPrice);
            command.Parameters.AddWithValue("@Quantity",orderDetail.Quantity);
            command.Parameters.AddWithValue("@Discount",orderDetail.Discount);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("OrderDetail đã được thêm thành công.");
            }
            else
            {
                Console.WriteLine("Không thể thêm Order.");
            }
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
           string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            {
                connection.Open();
                string sqlQuery = @"UPDATE OrderDetails
                                Set ProductID = @ProductID,
                                UnitPrice = @UnitPrice, 
                                Quantity = @Quantity ,
                                Discount = @Discount
                                WHERE OrderID = @OrderID";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@OrderID", orderDetail.OrderID);
                command.Parameters.AddWithValue("@ProductID", orderDetail.ProductID);
                command.Parameters.AddWithValue("@UnitPrice", orderDetail.UnitPrice);
                command.Parameters.AddWithValue("@Quantity",orderDetail.Quantity);
                command.Parameters.AddWithValue("@Discount",orderDetail.Discount);
                 int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("OrderDetail đã được cập nhật thành công.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy nhân viên có OrderDetail tương ứng.");
                    return;
                }
            }
            

        }
        public void DeleteOrdeDetail(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "Delete From OrderDetails where OrderID = @OrderID";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {

                command.Parameters.AddWithValue("@OrderID", id);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("OrderDetail  đã được xóa thành công.");
                }
                else
                {
                    Console.WriteLine("Không tìm có OrderID tương ứng.");
                }
            }
        }
    }
}