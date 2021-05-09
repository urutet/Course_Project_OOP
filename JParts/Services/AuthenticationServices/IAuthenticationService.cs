using JParts.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JParts.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<bool> Register(string client_ID, string name, string phone_Num, string address, string email,
            string login, string password, string confirmPassword);
        Task<Client> Login(string username, string password);
    }
}
