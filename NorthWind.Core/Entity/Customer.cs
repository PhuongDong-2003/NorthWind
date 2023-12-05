using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWind.Core.Entity
{
    [Table("Customers")]
    public class Customer
    {
        [Column(TypeName = "nchar(5)")]
        public string CustomerID { set; get; }

        [Column(TypeName = "nvarchar(40)")]
        public string CompanyName { set; get; }

        [Column(TypeName = "nvarchar(30)")]
        public string ContactName { set; get; }

        [Column(TypeName = "nvarchar(30)")]
        public string ContactTitle { set; get; }

        [Column(TypeName = "nvarchar(60)")]
        public string? Address { set; get; }

        [Column(TypeName = "nvarchar(15)")]
        public string? City { set; get; }

        [Column(TypeName = "nvarchar(15)")]
        public string? Region { set; get; }

        [Column(TypeName = "nvarchar(10)")]
        public string? PostalCode { set; get; }

        [Column(TypeName = "nvarchar(10)")]
        public string? Country { set; get; }

        [Column(TypeName = "nvarchar(15)")]
        public string? Phone { set; get; }

        [Column(TypeName = "nvarchar(24)")]
        public string? Fax { set; get; }

        [Column(TypeName = "nvarchar(24)")]
        public bool Status { set; get; }

        [Column(TypeName = "varchar(255)")]
        public string? Email { get; set; }

        public List<Order> orders;

        public Customer()
        {
            orders = new List<Order>();
        }

        [NotMapped]
        public int Page { get; set; }

        [NotMapped]
        public int PageSize { get; set; }

        [NotMapped]
        public long RowNum { get; set; }

        [NotMapped]
        public int TotalRow { get; set; }

        [NotMapped]
        public int TotalPages { get; set; }
    }
}