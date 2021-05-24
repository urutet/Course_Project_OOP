using JParts.DBContext;
using JParts.MVVM.Model;
using JParts.Repositories.Generic;
using JParts.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.Repositories.Implementations
{
    public class PartsOrdersRepository : Repository<PartsOrders>, IPartsOrdersRepository
    {
        public PartsOrdersRepository(JPartsContext context) : base(context)
        {

        }

        public JPartsContext JpartsContext { get { return Context as JPartsContext; } private set { } }
    }
}
