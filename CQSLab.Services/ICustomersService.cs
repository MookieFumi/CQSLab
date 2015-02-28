using CQSLab.Entities;
using CQSLab.Entities.Queries.Configuration;
using CQSLab.Entities.Queries.Result;

namespace CQSLab.Services
{
    public interface ICustomersService
    {
        void AddCustomer(Customer customer);
        Customer GetCustomer(int customerId);
        QueryResult<CustomerQueryResult> GetCustomers(QueryConfiguration configuration);
        void UpdateCustomer(Customer customer);
        void RemoveCustomer(int customerId);
    }
}