using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JParts.MVVM.Model
{
    public class Shop
    {
        public string ShopID { get; set; }

        [ForeignKey("Address")]
        public string AddressID { get; set; }
        public string Phone_Num { get; set; }

        public Shop()
        {

        }

        public Shop(string shop_ID, string address, string phone_Num)
        {
            ShopID = shop_ID;
            AddressID = address;
            Phone_Num = phone_Num;
        }
    }
}
