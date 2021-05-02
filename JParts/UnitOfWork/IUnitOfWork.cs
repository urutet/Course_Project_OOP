using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JParts.Repositories.Interfaces;

namespace JParts.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository Addresses { get; }
        ICarRepository Cars { get; }
        IClientRepository Clients { get; }
        IOrderRepository Orders { get; }
        IPartRepository Parts { get; }
        IShopRepository Shops { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}
