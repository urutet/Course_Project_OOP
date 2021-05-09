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
        public string AddressID { get; set; }
        public string Phone_Num { get; set; }

        public Address AddressOf { get; set; }

        public Shop()
        {

        }

        public Shop(string shop_ID, string address, string phone_Num)
        {
            ShopID = shop_ID;
            AddressID = address;
            Phone_Num = phone_Num;
        }

        public Shop(string shopID, string addressID, string phone_Num, Address addressOf)
        {
            ShopID = shopID;
            AddressID = addressID;
            Phone_Num = phone_Num;
            AddressOf = addressOf;
        }
    }
}
