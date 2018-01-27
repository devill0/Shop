using System;
using System.Collections.Generic;
using System.Text;
using Shop.Core.Domain;
using System.Linq;

namespace Shop.Core.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public static readonly ISet<Order> orders = new HashSet<Order>();

        public void Add(Order order)
            => orders.Add(order);

        public IEnumerable<Order> Browse(Guid userId)
            => orders.Where(x => x.UserId == userId);

        public Order Get(Guid id)
            => orders.SingleOrDefault(x => x.Id == id);
    }
}
