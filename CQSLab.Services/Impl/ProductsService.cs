using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async void AddProduct(Product product)
        {
            using (var tran = new TransactionScope())
            {
                Context.Products.Add(product);
                await Context.SaveChangesAsync();
                tran.Complete();
            }
        }

        public Task<Product> GetProduct(int productId)
        {
            return Context.Products
                .SingleOrDefaultAsync(p => p.ProductId == productId);
        }

        public Task<QueryResult<ProductQueryResult>> GetProducts(QueryConfiguration configuration)
        {
            var queries = new ProductsQueries(Context);
            return queries.GetProducts(configuration);
        }

        public async void RemoveProduct(int productId)
        {
            var product = await GetProduct(productId);
            Context.Products.Remove(product);
            await Context.SaveChangesAsync();
        }

        public async void UpdateProduct(Product product)
        {
            Context.Products.Attach(product);
            Context.Entry(product).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        #endregion
    }
}
