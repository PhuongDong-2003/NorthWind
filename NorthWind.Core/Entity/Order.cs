using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NorthWind.Core.Entity
{
    [Table("Orders")]
    public class Order
    {
        public int OrderID { set; get; }
        public string? CustomerID { set; get; }

        [ForeignKey("Employee")]
        public int? EmployeeID { set; get; }

        [JsonIgnore]
        public Employee Employee { set; get; }

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