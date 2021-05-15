using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JParts.MVVM.Model
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Phone_Num { get; set; }

        [ForeignKey("AddressOf")]
        public int AddressID { get; set; }
        public ICollection<Order> Orders { get; set; }
        public string Email { get; set; }

        [Key]
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; }

        public Address AddressOf { get; set; }

        public Client()
        {

        }

        public Client(string name, string phone_Num, int address, string email, string login, string password, bool isAdmin)
        {
            Name = name;
            Phone_Num = phone_Num;
            AddressID = address;
            Email = email;
            Login = login;
            PasswordHash = password;
            IsAdmin = isAdmin;
        }

        public Client(string name, string phone_Num, int addressID, ICollection<Order> orders, string email, string login, string passwordHash, Address addressOf)
        {
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
