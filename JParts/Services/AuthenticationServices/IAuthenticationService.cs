using JParts.Enums;
using JParts.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JParts.Services.AuthenticationServices
{

    public interface IAuthenticationService
    {
        public RegistrationResult Register(string client_ID, string name, string phone_Num,
            string addressID, int? House_Num, int? Flat_Num, string Street, string City, string email, string login, string password, string confirmPassword, bool isAdmin);
        Client Login(string username, string password);
    }
}
