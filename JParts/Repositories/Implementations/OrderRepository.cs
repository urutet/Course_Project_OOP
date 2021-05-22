using JParts.DBContext;
using JParts.MVVM.Model;
using JParts.Repositories.Generic;
using JParts.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

namespace JParts.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(JPartsContext context) : base(context)
        {

        }

        public JPartsContext JpartsContext { get { return Context as JPartsContext; } private set { } }

        public List<Order> GetAllOrders()
        {
            return JpartsContext.Orders.Distinct().ToList();
        }
    }
}
