using System.Data.Entity;
using System.Linq;
using System.Transactions;
using CQSLab.Business;
using CQSLab.Business.Entities;
using CQSLab.Business.Queries;
using CQSLab.Business.Queries.Configuration;
using CQSLab.Business.Queries.Result;

namespace CQSLab.Services.Impl
{
    public class ProductsService : ServiceBase, IProductsService
    {
        public ProductsService(ModelContext context)
            : base(context)
        {
        }

        #region IProductsService members

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

        public void RemoveProduct(int productId)
        {
            var product = GetProduct(productId);
            Context.Products.Remove(product);
            Context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            Context.Products.Attach(product);
            Context.Entry(product).State = EntityState.Modified;
            Context.SaveChanges();
        }

        #endregion
    }
}
