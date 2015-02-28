using System.Collections.Generic;
using CQSLab.Entities;
using CQSLab.Entities.Queries.Result;

namespace CQSLab.Services
{
    public interface ICustomersService
    {
        void AddCustomer(Customer customer);
        Customer GetCustomer(int customerId);
        IEnumerable<CustomerQueryResult> GetCustomers();
        void UpdateCustomer(Customer customer);
        void RemoveCustomer(int customerId);
    }
}