using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Repository
{
    public interface IOrderRepository
    {

        public IEnumerable<Order> GetOrder();
        public Order GetOrderByID(int orderID);
        public IEnumerable<Order> GetOrderByCustomerID(string CustomerID);
        public void InsertOrder(Order order);
        public void DeleteOrder(int id);
        public void UpdateOrder(Order order);
        public IEnumerable<Order> GetOrderPaged(int page, int pageSize);

        public void InsertOrderAccount(Order order);
        // public Order Find (int OrderID);

    }
}