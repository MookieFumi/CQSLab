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
    public class CustomersService : ServiceBase, ICustomersService
    {
        public CustomersService(ModelContext context)
            : base(context)
        {
        }

        #region ICustomersService members

        public void AddCustomer(Customer customer)
        {
            using (var tran = new TransactionScope())
            {
                Context.Customers.Add(customer);
                Context.SaveChanges();
                tran.Complete();
            }
        }

        public Customer GetCustomer(int customerId)
        {
            return Context.Customers.SingleOrDefault(p => p.CustomerId == customerId);
        }

        public QueryResult<CustomerQueryResult> GetCustomers(QueryConfiguration configuration)
        {
            var queries = new CustomersQueries(Context);
            return queries.GetCustomers(configuration);
        }

        public void RemoveCustomer(int customerId)
        {
            var customer = GetCustomer(customerId);
            Context.Customers.Remove(customer);
            Context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            Context.Customers.Attach(customer);
            Context.Entry(customer).State = EntityState.Modified;
            Context.SaveChanges();
        }

        #endregion
    }
}