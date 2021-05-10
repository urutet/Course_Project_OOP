using JParts.MVVM.Model;
using JParts.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JParts.Repositories.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> GetByUsername(string username);
        Task<Client> GetByEmail(string email);
    }
}
