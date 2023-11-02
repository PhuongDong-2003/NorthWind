using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration _configuration;

        public AccountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       

        public IEnumerable<Account> GetAll()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sqlQuery = "SELECT * FROM Accounts";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var resultList = new List<Account>();
                    while (reader.Read())
                    {
                        Account account = new Account
                        {
                            
                            UserName =  reader.IsDBNull(reader.GetOrdinal("UserName")) ? null : reader.GetString(reader.GetOrdinal("UserName")),
                            PassWord = reader.GetInt32(reader.GetOrdinal("Password"))
                            
                        };
                        resultList.Add(account);
                    }

                    return resultList;
                }


            }

        }
    }
}