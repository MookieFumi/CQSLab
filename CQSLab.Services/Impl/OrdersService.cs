using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using CQSLab.Business;
using CQSLab.Business.Entities;

namespace CQSLab.Services.Impl
{
    public class OrdersService : ServiceBase, IOrdersService
    {
        public OrdersService(ModelContext context)
            : base(context)
        {
        }

        #region IOrdersService members

        public void AddOrder(Order order)
        {
            using (var tran = new TransactionScope())
            {
                Context.Orders.Add(order);
                Context.SaveChanges();
                tran.Complete();
            }
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

        public Order GetOrder(int orderId)
        {
            return Context.Orders.SingleOrDefault(p => p.OrderId == orderId);
        }

        public IEnumerable<Order> GetOrders()
        {
            return Context.Orders.ToList();
        }

        public void RemoveOrder(int orderId)
        {
            var order = GetOrder(orderId);
            Context.Orders.Remove(order);
            Context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            Context.Orders.Attach(order);
            Context.Entry(order).State = EntityState.Modified;
            Context.SaveChanges();
        }

        #endregion
    }
}