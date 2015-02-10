using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using CQSLab.Entities;

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

        public IEnumerable<Customer> GetCustomers()
        {
            return Context.Customers.ToList();
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