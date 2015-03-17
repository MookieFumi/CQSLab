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
    public class CustomersService : ServiceBase, ICustomersService
    {
        public CustomersService(ModelContext context)
            : base(context)
        {
        }

        #region ICustomersService members

        public async void AddCustomer(Customer customer)
        {
            using (var tran = new TransactionScope())
            {
                Context.Customers.Add(customer);
                await Context.SaveChangesAsync();
                tran.Complete();
            }
        }

        public Task<Customer> GetCustomer(int customerId)
        {
            return Context.Customers
                .SingleOrDefaultAsync(p => p.CustomerId == customerId);
        }

        public Task<QueryResult<CustomerQueryResult>> GetCustomers(QueryConfiguration configuration)
        {
            var queries = new CustomersQueries(Context);
            return queries.GetCustomers(configuration);
        }

        public async void RemoveCustomer(int customerId)
        {
            var customer = await GetCustomer(customerId);
            Context.Customers.Remove(customer);
            await Context.SaveChangesAsync();
        }

        public async void UpdateCustomer(Customer customer)
        {
            Context.Customers.Attach(customer);
            Context.Entry(customer).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        #endregion
    }
}