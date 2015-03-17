using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async void AddOrder(Order order)
        {
            using (var tran = new TransactionScope())
            {
                Context.Orders.Add(order);
                await Context.SaveChangesAsync();
                tran.Complete();
            }
        }

        public async void AddOrderLine(OrderLine orderLine)
        {
            using (var tran = new TransactionScope())
            {
                Context.OrderLines.Add(orderLine);
                await Context.SaveChangesAsync();
                tran.Complete();
            }
        }

        public Task<Order> GetOrder(int orderId)
        {
            return Context.Orders
                .SingleOrDefaultAsync(p => p.OrderId == orderId);
        }

        public Task<List<Order>> GetOrders()
        {
            return Context.Orders.ToListAsync();
        }

        public async void RemoveOrder(int orderId)
        {
            var order = await GetOrder(orderId);
            Context.Orders.Remove(order);
            await Context.SaveChangesAsync();
        }

        public async void UpdateOrder(Order order)
        {
            Context.Orders.Attach(order);
            Context.Entry(order).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        #endregion
    }
}