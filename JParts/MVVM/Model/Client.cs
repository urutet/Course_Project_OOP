using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JParts.MVVM.Model
{
    public class Client
    {
        public string ClientID { get; set; }
        public string Name { get; set; }
        public string Phone_Num { get; set; }

        [ForeignKey("Address")]
        public string AddressID { get; set; }
        public ICollection<Order> Orders { get; set; }
        public string Email { get; set; }
        private string Login { get; set; }
        private string PasswordHash { get; set; }

        public Client()
        {

        }

        public Client(string client_ID, string name, string phone_Num, string address, string email, string login, string password)
        {
            ClientID = client_ID;
            Name = name;
            Phone_Num = phone_Num;
            AddressID = address;
            Email = email;
            Login = login;
            PasswordHash = password;
        }
    }
}
