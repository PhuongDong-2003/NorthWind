using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NorthWind.Core.Entity
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { set; get; }

        [ForeignKey("customer"), MaxLength(5)]
        public string? CustomerID { set; get; } 

        [JsonIgnore]  
        public Customer customer; 

        [ForeignKey("Employee")]
        public int? EmployeeID { set; get; }

        [JsonIgnore]
        public Employee Employee { set; get; }

        public  List<OrderDetail> OrderDetails { set; get; }

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
        
        [JsonIgnore]
        public Shipper Shipper;

        public DateTime? OrderDate { set; get; }
        public DateTime? RequiredDate { set; get; }
        public DateTime? ShippedDate { set; get; }
        public int? ShipVia { set; get; }
        public decimal? Freight { set; get; }

        [Column(TypeName = "nvarchar(40)")]
        public string? ShipName { set; get; }

        [Column(TypeName = "nvarchar(60)")]
        public string? ShipAddress { set; get; }

        [Column(TypeName = "nvarchar(15)")]
        public string? ShipCity { set; get; }

        [Column(TypeName = "nvarchar(15)")]
        public string? ShipRegion { set; get; }

        [Column(TypeName = "nvarchar(10)")]
        public string? ShipPostalCode { set; get; }

        [Column(TypeName = "nvarchar(15)")]
        public string? ShipCountry { set; get; }

        public bool Status { set; get; }
    
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

            public void CalculatePagination(int pageSize)
        {
        
            if (pageSize <= 0)
            {
                return;
            }


            TotalPages = (int)Math.Ceiling((double)TotalRow / pageSize);

            
            Page = Math.Min(Page, TotalPages);

        
            Page = Math.Max(Page, 1);

        
            RowNum = (Page - 1) * pageSize + 1;
        }
    }
}