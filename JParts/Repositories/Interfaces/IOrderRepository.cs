using JParts.MVVM.Model;
using JParts.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        public List<Order> GetAllOrders();
        public List<Order> GetClientOrders(int clientID);
    }
}
