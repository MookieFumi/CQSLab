using CQSLab.Business.Entities;
using CQSLab.Business.Queries.Configuration;
using CQSLab.Business.Queries.Result;

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