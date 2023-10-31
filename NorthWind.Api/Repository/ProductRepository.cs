using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public IEnumerable<Product> GetProduct()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "SELECT * FROM Products";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var resultList = new List<Product>();
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            SupplierID = reader.IsDBNull(reader.GetOrdinal("SupplierID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SupplierID")),
                            CategoryID = reader.IsDBNull(reader.GetOrdinal("CategoryID")) ? null : reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            QuantityPerUnit = reader.IsDBNull(reader.GetOrdinal("QuantityPerUnit")) ? null : reader.GetString(reader.GetOrdinal("QuantityPerUnit")),
                            UnitPrice = reader.IsDBNull(reader.GetOrdinal("UnitPrice")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                            UnitsInStock = reader.IsDBNull(reader.GetOrdinal("UnitsInStock")) ? (short?)null : reader.GetInt16(reader.GetOrdinal("UnitsInStock")),
                            UnitsOnOrder = reader.IsDBNull(reader.GetOrdinal("UnitsOnOrder")) ? (short?)null : reader.GetInt16(reader.GetOrdinal("UnitsOnOrder")),
                            ReorderLevel = reader.IsDBNull(reader.GetOrdinal("ReorderLevel")) ? (short?)null : reader.GetInt16(reader.GetOrdinal("ReorderLevel")),
                            Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued")),
                            Status = reader.GetBoolean(reader.GetOrdinal("Status"))
                        };
                        resultList.Add(product);
                    }

                    return resultList;
                }
            }
        }

        public Product GetProductByID(int ProductID)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "SELECT * FROM Products WHERE ProductID = @ProductID and Status = 0";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@ProductID", ProductID);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            SupplierID = reader.IsDBNull(reader.GetOrdinal("SupplierID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SupplierID")),
                            CategoryID = reader.IsDBNull(reader.GetOrdinal("CategoryID")) ? null : reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            QuantityPerUnit = reader.IsDBNull(reader.GetOrdinal("QuantityPerUnit")) ? null : reader.GetString(reader.GetOrdinal("QuantityPerUnit")),
                            UnitPrice = reader.IsDBNull(reader.GetOrdinal("UnitPrice")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                            UnitsInStock = reader.IsDBNull(reader.GetOrdinal("UnitsInStock")) ? (short?)null : reader.GetInt16(reader.GetOrdinal("UnitsInStock")),
                            UnitsOnOrder = reader.IsDBNull(reader.GetOrdinal("UnitsOnOrder")) ? (short?)null : reader.GetInt16(reader.GetOrdinal("UnitsOnOrder")),
                            ReorderLevel = reader.IsDBNull(reader.GetOrdinal("ReorderLevel")) ? (short?)null : reader.GetInt16(reader.GetOrdinal("ReorderLevel")),
                            Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued")),
                            Status = reader.GetBoolean(reader.GetOrdinal("Status"))
                        };
                        return product;

                    }
                    else
                    {
                        return null;
                    }


                }
            }
        }

        public IEnumerable<Product> GetProductPaged(int page, int pageSize)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            var resultList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = @"
                            Select *
                            from (
                                SELECT *, ROW_NUMBER() OVER ( ORDER BY ProductID) row_num,
                                Count(1) OVER () AS TotalRow  
                                FROM Products where Status = 0
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
                            Product product = new Product
                            {
                                ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                SupplierID = reader.IsDBNull(reader.GetOrdinal("SupplierID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SupplierID")),
                                CategoryID = reader.IsDBNull(reader.GetOrdinal("CategoryID")) ? null : reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                QuantityPerUnit = reader.IsDBNull(reader.GetOrdinal("QuantityPerUnit")) ? null : reader.GetString(reader.GetOrdinal("QuantityPerUnit")),
                                UnitPrice = reader.IsDBNull(reader.GetOrdinal("UnitPrice")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                                UnitsInStock = reader.IsDBNull(reader.GetOrdinal("UnitsInStock")) ? (short?)null : reader.GetInt16(reader.GetOrdinal("UnitsInStock")),
                                UnitsOnOrder = reader.IsDBNull(reader.GetOrdinal("UnitsOnOrder")) ? (short?)null : reader.GetInt16(reader.GetOrdinal("UnitsOnOrder")),
                                ReorderLevel = reader.IsDBNull(reader.GetOrdinal("ReorderLevel")) ? (short?)null : reader.GetInt16(reader.GetOrdinal("ReorderLevel")),
                                Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued")),
                                Status = reader.GetBoolean(reader.GetOrdinal("Status")),
                                Page = page,
                                PageSize = pageSize,
                                RowNum = reader.GetInt64(reader.GetOrdinal("row_num")),
                                TotalRow = reader.GetInt32(reader.GetOrdinal("TotalRow")),
                                TotalPages = (int)Math.Ceiling((double)reader.GetInt32(reader.GetOrdinal("TotalRow")) / pageSize)
                            };
                            resultList.Add(product);
                        }
                    }
                }

                return resultList;

            }
        }

        public void InsertProduct(Product product)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "INSERT INTO Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued, Status) " +
                              "VALUES (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued, @Status)";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@SupplierID", product.SupplierID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CategoryID", product.CategoryID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@QuantityPerUnit", product.QuantityPerUnit ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UnitsInStock", product.UnitsInStock ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UnitsOnOrder", product.UnitsOnOrder ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ReorderLevel", product.ReorderLevel ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Discontinued", product.Discontinued);
                command.Parameters.AddWithValue("@Status", false);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(Product product)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "UPDATE Products " +
                              "SET ProductName = @ProductName, SupplierID = @SupplierID, CategoryID = @CategoryID, " +
                              "QuantityPerUnit = @QuantityPerUnit, UnitPrice = @UnitPrice, UnitsInStock = @UnitsInStock, " +
                              "UnitsOnOrder = @UnitsOnOrder, ReorderLevel = @ReorderLevel, Discontinued = @Discontinued, Status = @Status " +
                              "WHERE ProductID = @ProductID";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@ProductID", product.ProductID);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@SupplierID", product.SupplierID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CategoryID", product.CategoryID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@QuantityPerUnit", product.QuantityPerUnit ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UnitsInStock", product.UnitsInStock ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UnitsOnOrder", product.UnitsOnOrder ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ReorderLevel", product.ReorderLevel ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Discontinued", product.Discontinued);
                command.Parameters.AddWithValue("@Status", false);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Product đã được cập nhật thành công.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy nhân viên có Product tương ứng.");
                    return;
                }
            }
        }

        public void DeleteProduct(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "UPDATE Products SET Status = 1 WHERE ProductID = @ProductID";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@ProductID", id);
                command.ExecuteNonQuery();
            }

        }
    }
}