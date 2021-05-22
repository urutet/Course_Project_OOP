using JParts.DBContext;
using JParts.MVVM.Model;
using JParts.Repositories.Generic;
using JParts.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace JParts.Repositories.Implementations
{
    public class ShopRepository : Repository<Shop>, IShopRepository
    {
        public ShopRepository(JPartsContext context) : base(context)
        {

        }

        public JPartsContext JpartsContext { get { return Context as JPartsContext; }}
    }
}
