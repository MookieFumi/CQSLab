using CQSLab.Entities;
using CQSLab.Entities.Queries.Configuration;
using CQSLab.Entities.Queries.Result;

namespace CQSLab.Services
{
    public interface IProductsService
    {
        void AddProduct(Product product);
        Product GetProduct(int productId);
        QueryResult<ProductQueryResult> GetProducts(QueryConfiguration configuration);
        void UpdateProduct(Product product);
        void RemoveProduct(int productId);
    }
}