using JParts.Enums;
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

        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(UnitOfWork.UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = new PasswordHasher();
        }

        public async Task<Client> Login(string username, string password)
        {
            Client storedClient = await _unitOfWork.Clients.GetByUsername(username);

            PasswordVerificationResult passwordsMatch = _passwordHasher.VerifyHashedPassword(storedClient.PasswordHash, password);

            if(passwordsMatch != PasswordVerificationResult.Success)
            {
                throw new Exception();
            }

            return storedClient;
        }

        public async Task<RegistrationResult> Register(string client_ID, string name, string phone_Num,
            string addressID, int? House_Num, int? Flat_Num, string Street, string City, string email, string login, string password, string confirmPassword)
        {
            RegistrationResult result = RegistrationResult.Success;

            if (await _unitOfWork.Clients.GetByUsername(login) != null)
            {
                result = RegistrationResult.LoginAlreadyExists;
            }
            else if (password == confirmPassword && result == RegistrationResult.Success)
            {
                Address addr = new Address(Convert.ToString(House_Num + Flat_Num) + Street, City, Street, House_Num, Flat_Num);
                _unitOfWork.Addresses.Add(addr);
                _unitOfWork.Complete();

                string hashedPassword = _passwordHasher.HashPassword(password);

                Client client = new Client(client_ID, name, phone_Num, addressID, email, login, hashedPassword);

                _unitOfWork.Clients.Add(client);
                await _unitOfWork.CompleteAsync();
            }
            else if(password != confirmPassword)
            {
                result = RegistrationResult.PasswordDoNotMatch;
            }

            return result;
        }
    }
}
