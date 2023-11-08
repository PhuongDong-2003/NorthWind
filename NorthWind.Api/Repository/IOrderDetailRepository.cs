using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Repository
{
    public interface IOrderDetailRepository
    {
        public IEnumerable<OrderDetail> GetOrderDetails();
        public OrderDetail GetOrderDetailsByID(int orderID);
        public void InsertOrderDetail(OrderDetail orderDetail);
        public void DeleteOrdeDetail(int id);
        public void UpdateOrderDetail(OrderDetail orderDetail);

    }
}