using JParts.Repositories.Interfaces;
using JParts.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using JParts.DBContext;

namespace JParts.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JPartsContext _context;

        public UnitOfWork(JPartsContext context)
        {
            _context = context;
            Addresses = new AddressRepository(_context);
            Cars = new CarRepository(_context);
            Clients = new ClientRepository(_context);
            Orders = new OrderRepository(_context);
            Parts = new PartRepository(_context);
            Shops = new ShopRepository(_context);
        }

        public IAddressRepository Addresses { get; private set; }

        public ICarRepository Cars { get; private set; }

        public IClientRepository Clients { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IPartRepository Parts { get; private set; }

        public IShopRepository Shops { get; private set; }

        int IUnitOfWork.Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
