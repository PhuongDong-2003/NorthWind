using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWind.Core.Entity
{
    [Table("Shippers")]
    public class Shipper
    {

    [Key]
    public int ShipperId { get; set; }
    
    [MaxLength (40)]
    public string CompanyName { get; set; } = null!;

    [MaxLength (24)]
    public string? Phone { get; set; }

    public List<Order> Orders { get; set; } 

    public Shipper()
    {
        Orders = new List<Order>();
    }
    
    }
}