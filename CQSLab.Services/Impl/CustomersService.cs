using CQSLab.Entities;
using CQSLab.Entities.Queries;
using CQSLab.Entities.Queries.Configuration;
using CQSLab.Entities.Queries.Result;
using System.Data.Entity;
using System.Linq;
using System.Transactions;

namespace CQSLab.Services
{
    public class CustomersService : ServiceBase, ICustomersService
    {
        public CustomersService(ModelContext context)
            : base(context)
        {
        }

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

        public void UpdateCustomer(Customer customer)
        {
            Context.Customers.Attach(customer);
            Context.Entry(customer).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void RemoveCustomer(int customerId)
        {
            var customer = GetCustomer(customerId);
            Context.Customers.Remove(customer);
            Context.SaveChanges();
        }
    }
}