using System.Collections.Generic;
using CQSLab.Entities;

namespace CQSLab.Services
{
    public interface ICustomersService
    {
        void AddCustomer(Customer customer);
        Customer GetCustomer(int customerId);
        IEnumerable<Customer> GetCustomers();
        void UpdateCustomer(Customer customer);
        void RemoveCustomer(int customerId);
    }
}