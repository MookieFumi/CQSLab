using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using CQS.Entities;

namespace CQSLab.Services
{
    public class OrdersService : ServiceBase, IOrdersService
    {
        public OrdersService(ModelContext context)
            : base(context)
        {
        }

        public void AddOrder(Order order)
        {
            using (var tran = new TransactionScope())
            {
                Context.Orders.Add(order);
                Context.SaveChanges();
                tran.Complete();
            }
        }

        public Order GetOrder(int orderId)
        {
            return Context.Orders.SingleOrDefault(p => p.OrderId == orderId);
        }

        public IEnumerable<Order> GetOrders()
        {
            return Context.Orders.ToList();
        }

        public void UpdateOrder(Order order)
        {
            Context.Orders.Attach(order);
            Context.Entry(order).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void RemoveOrder(int orderId)
        {
            var order = GetOrder(orderId);
            Context.Orders.Remove(order);
            Context.SaveChanges();
        }

        public void AddOrderLine(OrderLine orderLine)
        {
            using (var tran = new TransactionScope())
            {
                Context.OrderLines.Add(orderLine);
                Context.SaveChanges();
                tran.Complete();
            }
        }
    }
}