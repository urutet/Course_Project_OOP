using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.MVVM.Model
{
    public class Client
    {
        public string Client_ID { get; set; }
        public string Name { get; set; }
        public string Phone_Num { get; set; }
        public Address address { get; set; }
        public string Email { get; set; }
        private string Login { get; set; }
        private string Password { get; set; }

        public Client()
        {

        }

        public Client(string client_ID, string name, string phone_Num, Address address, string email, string login, string password)
        {
            Client_ID = client_ID;
            Name = name;
            Phone_Num = phone_Num;
            this.address = address;
            Email = email;
            Login = login;
            Password = password;
        }
    }
}
