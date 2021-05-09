using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JParts.MVVM.Model
{
    public class Client
    {
        [Key]
        public string ClientID { get; set; }
        public string Name { get; set; }
        public string Phone_Num { get; set; }

        [ForeignKey("AddressOf")]
        public string AddressID { get; set; }
        public ICollection<Order> Orders { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public Address AddressOf { get; set; }

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

        public Client(string clientID, string name, string phone_Num, string addressID, ICollection<Order> orders, string email, string login, string passwordHash, Address addressOf)
        {
            ClientID = clientID;
            Name = name;
            Phone_Num = phone_Num;
            AddressID = addressID;
            Orders = orders;
            Email = email;
            Login = login;
            PasswordHash = passwordHash;
            AddressOf = addressOf;
        }
    }
}
