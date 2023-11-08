using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Repository
{
    public interface IProductRepository
    {

        public IEnumerable<Product> GetProduct();
        public Product GetProductByID(int ProductID);
        public Product GetProductByName(string ProductName);
        public void InsertProduct(Product product);
        public void DeleteProduct(int id);
        public void UpdateProduct(Product product);
        public IEnumerable<Product> GetProductPaged(int page, int pageSize);

    }
}