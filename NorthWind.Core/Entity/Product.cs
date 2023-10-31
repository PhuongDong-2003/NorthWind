using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWind.Core.Entity
{
  public class Product
  {
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public int? SupplierID { get; set; }
    public int? CategoryID { get; set; }
    public string? QuantityPerUnit { get; set; }
    public decimal? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public short? UnitsOnOrder { get; set; }
    public short? ReorderLevel { get; set; }
    public bool Discontinued { get; set; }
    public bool Status {get; set;}
    public int Page { get; set; }
    public int PageSize { get; set; }
    public long RowNum { get; set; }
    public int TotalRow { get; set; }
    public int TotalPages { get; set; }
  }
}