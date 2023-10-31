using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthWind.Core.Entity;

namespace NorthWind.Api.Repository
{
    public interface IProductRepository
    {

        public IEnumerable<Product> GetProduct();
        public Product GetProductByID(int ProductID);
        public void InsertProduct(Product product);
        public void DeleteProduct(int id);
        public void UpdateProduct(Product product);
        public IEnumerable<Product> GetProductPaged(int page, int pageSize);

    }
}