using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Repository
{
    public interface ICustomerRepository
    {
   
        public IEnumerable<Customer> GetCustomer();
        public Customer GetCustomerByID(string customerId);
        public void InsertCustomer(Customer customer);
        public void DeleteCustomer(string id);
        public void UpdateCustomer(Customer customer);
        public IEnumerable<Customer> GetCustomerPaged(int page, int pageSize);
    }
}