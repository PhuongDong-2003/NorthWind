using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWind.Core.Entity
{
    public class Customer
    {
        public string CustomerID { set; get; }
        public string CompanyName { set; get; }
        public string? ContactName { set; get; }
        public string? ContactTitle { set; get; }
        public string? Address { set; get; }
        public string? City { set; get; }
        public string? Region { set; get; }
        public string? PostalCode { set; get; }
        public string? Country { set; get; }
        public string? Phone { set; get; }
        public string? Fax { set; get; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int RowNum { get; set; }
        public int TotalRow { get; set; }
        public int TotalPages { get; set; }
    }
}