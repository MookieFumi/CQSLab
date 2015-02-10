using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using CQSLab.Entities;

namespace CQSLab.Services
{
    public class ProductsService : ServiceBase, IProductsService
    {
        public ProductsService(ModelContext context) : base(context)
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

        public IEnumerable<Product> GetProducts()
        {
            return Context.Products.ToList();
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
