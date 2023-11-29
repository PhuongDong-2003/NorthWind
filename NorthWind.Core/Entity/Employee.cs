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
        
        [StringLength(20)]
        public string LastName { get; set; } = null!;

        [StringLength(10)]
        public string FirstName { get; set; } = null!;

        [StringLength(30)]
        public string? Title { get; set; }

        [StringLength(25)]
        public string? TitleOfCourtesy { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [StringLength(60)]
        public string? Address { get; set; }

        [StringLength(15)]
        public string? City { get; set; }

        [StringLength(15)]
        public string? Region { get; set; }

        [StringLength(10)]
        public string? PostalCode { get; set; }

         [StringLength(15)]
        public string? Country { get; set; }

        [StringLength(24)]
        public string? HomePhone { get; set; }

        [StringLength(4)]
        public string? Extension { get; set; }

        public byte[]? Photo { get; set; }

        [StringLength(225)] 
        public string? Notes { get; set; }

        [ForeignKey("EmployeeId")]
        public int? ReportsTo { get; set; }

        [StringLength(225)]
        public string? PhotoPath { get; set; }

        
        public  List<Order> Orders { get; set;}

          public Employee()
        {
            Orders = new List<Order>();
        }

        [ForeignKey("ReportsTo")]
        public virtual Employee? ReportsToNavigation { get; set; }

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