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
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(JPartsContext context) : base(context)
        {

        }

        public JPartsContext JpartsContext { get { return Context as JPartsContext; } private set { } }

        public Client GetByEmail(string email)
        {
            return JpartsContext.Clients.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }

        public Client GetByID(int id)
        {
            return JpartsContext.Clients.AsNoTracking().FirstOrDefault(c => c.ClientID == id);
        }

        public Client GetByUsername(string username)
        {
            return JpartsContext.Clients.AsNoTracking().FirstOrDefault(c => c.Login == username);

        }
    }
}
