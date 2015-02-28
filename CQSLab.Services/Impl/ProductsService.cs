using CQSLab.Entities;
using CQSLab.Entities.Queries;
using CQSLab.Entities.Queries.Configuration;
using CQSLab.Entities.Queries.Result;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;

namespace CQSLab.Services
{
    public class ProductsService : ServiceBase, IProductsService
    {
        public ProductsService(ModelContext context)
            : base(context)
        {
        }

        public void AddProduct(Product product)
        {
            using (var tran = new TransactionScope())
            {
                Context.Products.Add(product);
                Context.SaveChanges();
                tran.Complete();
            }
        }

        public Product GetProduct(int productId)
        {
            return Context.Products.SingleOrDefault(p => p.ProductId == productId);
        }

        public QueryResult<ProductQueryResult> GetProducts(QueryConfiguration configuration)
        {
            var queries = new ProductsQueries(Context);
            return queries.GetProducts(configuration);
        }

        public void UpdateProduct(Product product)
        {
            Context.Products.Attach(product);
            Context.Entry(product).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void RemoveProduct(int productId)
        {
            var product = GetProduct(productId);
            Context.Products.Remove(product);
            Context.SaveChanges();
        }
    }
}
