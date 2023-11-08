using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWind.Core.Entity
{
    public class Order
    {
        public int OrderID { set; get; }
        public string? CustomerID { set; get; }
        public int? EmployeeID { set; get; }
        public DateTime? OrderDate { set; get; }
        public DateTime? RequiredDate { set; get; }
        public DateTime? ShippedDate { set; get; }
        public int? ShipVia { set; get; }
        public decimal? Freight { set; get; }
        public string? ShipName { set; get; }
        public string? ShipAddress { set; get; }
        public string? ShipCity { set; get; }
        public string? ShipRegion { set; get; }
        public string? ShipPostalCode { set; get; }
        public string? ShipCountry { set; get; }
       
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long RowNum { get; set; }
        public int TotalRow { get; set; }
        public int TotalPages { get; set; }
    }
}