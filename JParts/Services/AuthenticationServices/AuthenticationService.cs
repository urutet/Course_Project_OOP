using JParts.MVVM.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JParts.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UnitOfWork.UnitOfWork _unitOfWork;

        public AuthenticationService(UnitOfWork.UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Client> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Register(string client_ID, string name, string phone_Num,
            string address, string email, string login, string password, string confirmPassword)
        {
            bool status = false;
            if (password == confirmPassword)
            {

                IPasswordHasher hasher = new PasswordHasher();

                string hashedPassword = hasher.HashPassword(password);

                Client client = new Client(client_ID, name, phone_Num, address, email, login, password);

                _unitOfWork.Clients.Add(client);
                _unitOfWork.Complete();
            }

            return status;
        }
    }
}
