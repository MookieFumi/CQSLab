using System.Collections.Generic;
using CQS.Entities;

namespace CQSLab.Services
{
    public interface IProductsService
    {
        void AddProduct(Product product);
        Product GetProduct(int productId);
        IEnumerable<Product> GetProducts();
        void UpdateProduct(Product product);
        void RemoveProduct(int productId);
    }
}