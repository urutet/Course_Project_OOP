using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JParts.MVVM.Model
{
    public class Shop
    {
        public string ShopID { get; set; }

        [ForeignKey("AddressOf")]
        public int AddressID { get; set; }
        public string Phone_Num { get; set; }

        public Address AddressOf { get; set; }

        public Shop()
        {

        }

        public Shop(int address, string phone_Num)
        {
            AddressID = address;
            Phone_Num = phone_Num;
        }

        public Shop(int addressID, string phone_Num, Address addressOf)
        {
            AddressID = addressID;
            Phone_Num = phone_Num;
            AddressOf = addressOf;
        }
    }
}
