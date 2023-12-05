using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NorthWind.Core.Entity
{
    [Table("Employees")]
    public class Employee
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("EmployeeID")]
        public int EmployeeId { get; set; }
        
        [Column(TypeName = "nvarchar(20)")]
        public string LastName { get; set; } = null!;

        [Column(TypeName = "nvarchar(10)")]
        public string FirstName { get; set; } = null!;

        [Column(TypeName = "nvarchar(30)")]
        public string? Title { get; set; }

        [Column(TypeName = "nvarchar(25)")]
        public string? TitleOfCourtesy { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [Column(TypeName = "nvarchar(60)")]
        public string? Address { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        public string? City { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        public string? Region { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        public string? PostalCode { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        public string? Country { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public string? HomePhone { get; set; }

        [Column(TypeName = "nvarchar(4)")]
        public string? Extension { get; set; }

        public byte[]? Photo { get; set; }

       
        public string? Notes { get; set; }

        public int? ReportsTo { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? PhotoPath { get; set; }

        public bool Status { set; get; }
    
        public  List<Order> Orders { get; set;}

          public Employee()
        {
            Orders = new List<Order>();
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