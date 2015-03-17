using System.Threading.Tasks;
using CQSLab.Business.Entities;
using CQSLab.Business.Queries.Configuration;
using CQSLab.Business.Queries.Result;

namespace CQSLab.Services
{
    public interface ICustomersService
    {
        void AddCustomer(Customer customer);
        Task<Customer> GetCustomer(int customerId);
        Task<QueryResult<CustomerQueryResult>> GetCustomers(QueryConfiguration configuration);
        void UpdateCustomer(Customer customer);
        void RemoveCustomer(int customerId);
    }
}