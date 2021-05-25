using JParts.DBContext;
using JParts.MVVM.Model;
using JParts.Repositories.Generic;
using JParts.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JParts.Repositories.Implementations
{
    public class PartRepository : Repository<Part>, IPartRepository
    {
        public PartRepository(JPartsContext context) : base(context)
        {

        }

        public JPartsContext JpartsContext { get { return Context as JPartsContext; } private set { } }

        public List<Part> GetAllParts()
        {
            return JpartsContext.Parts.Distinct().ToList();
        }

        public List<PartsOrders> GetPartsByOrderID(int orderID)
        {
            return JpartsContext.PartsOrders.Where(p => p.OrderID == orderID).Include(p => p.Part).ToList();
        }
    }
}
