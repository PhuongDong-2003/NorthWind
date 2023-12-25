using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NorthWind.Core.Entity
{
  [Table("OrderDetails")]
  public class OrderDetail
  {
    [Key, ForeignKey("Order")]
    public int OrderID { get; set; }
    
    [JsonIgnore]
    public Order Order { get; set; } = null!;

    [Key, ForeignKey("Product")]
    public int ProductID { get; set; }
    
    [JsonIgnore]
    public Product Product { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
    public float Discount { get; set; }

  }
}