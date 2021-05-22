using JParts.DBContext;
using JParts.MVVM.Model;
using JParts.Repositories.Generic;
using JParts.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Linq;

namespace JParts.Repositories.Implementations
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(JPartsContext context) : base(context)
        {
            
        }

        public JPartsContext JpartsContext { get { return Context as JPartsContext; } private set { } }

        public Address GetAddressByObj(Address address)
        {
            return JpartsContext.Adresses.AsNoTracking().First(a => a.City == address.City && a.Street == address.Street && a.House_Num == address.House_Num && a.Flat_Num == address.Flat_Num);
        }
    }
}
