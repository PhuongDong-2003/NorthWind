using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWind.Core.Entity
{

  [Table("Products")]
  public class Product
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductID { get; set; }

    [Column(TypeName = "nvarchar(40)")]
    public string ProductName { get; set; }
    public int? SupplierID { get; set; }
    public int? CategoryID { get; set; }

    [Column(TypeName = "nvarchar(20)")]
    public string? QuantityPerUnit { get; set; }
    public decimal? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public short? UnitsOnOrder { get; set; }
    public short? ReorderLevel { get; set; }
    public bool Discontinued { get; set; }
    public bool Status {get; set;}

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

    [NotMapped]
    public List<OrderDetail> OrderDetails { set; get; }

    public Product()
    {
      OrderDetails = new List<OrderDetail>();
    }
  }
}