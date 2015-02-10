using System.Collections.Generic;
using CQSLab.Entities;

namespace CQSLab.Services
{
    public interface IOrdersService
    {
        void AddOrder(Order order);
        Order GetOrder(int orderId);
        IEnumerable<Order> GetOrders();
        void UpdateOrder(Order order);
        void RemoveOrder(int orderID);

        //OrderLines
        void AddOrderLine(OrderLine orderLine);
    }
}