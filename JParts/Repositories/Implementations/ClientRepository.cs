using JParts.DBContext;
using JParts.MVVM.Model;
using JParts.Repositories.Generic;
using JParts.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<Client> GetByEmail(string email)
        {
            return await JpartsContext.Clients.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Client> GetByUsername(string username)
        {
            return await JpartsContext.Clients.FirstOrDefaultAsync(c => c.Login == username);

        }
    }
}
