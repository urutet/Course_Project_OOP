using JParts.MVVM.Model;
using JParts.Repositories.Generic;
using JParts.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace JParts.Repositories.Implementations
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DbContext context) : base(context)
        {

        }

        public DbContext dbContext { get { return Context as DbContext; } private set { } }
    }
}
