using System.Collections.Generic;
using System.Threading.Tasks;
using CQSLab.Business.Entities;

namespace CQSLab.Services
{
    public interface IOrdersService
    {
        void AddOrder(Order order);
        Task<Order> GetOrder(int orderId);
        Task<List<Order>> GetOrders();
        void UpdateOrder(Order order);
        void RemoveOrder(int orderID);

        //OrderLines
        void AddOrderLine(OrderLine orderLine);
    }
}