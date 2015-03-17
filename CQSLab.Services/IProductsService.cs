using System.Threading.Tasks;
using CQSLab.Business.Entities;
using CQSLab.Business.Queries.Configuration;
using CQSLab.Business.Queries.Result;

namespace CQSLab.Services
{
    public interface IProductsService
    {
        void AddProduct(Product product);
        Task<Product> GetProduct(int productId);
        Task<QueryResult<ProductQueryResult>> GetProducts(QueryConfiguration configuration);
        void UpdateProduct(Product product);
        void RemoveProduct(int productId);
    }
}