using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWind.Core.Entity
{
    public class OrderDetail
    {
      public int OrderID {get; set;}
      public int ProductID {get; set;}
      public decimal UnitPrice {get; set;}
      public short Quantity {get; set;}
      public float Discount {get; set;}
    }
}