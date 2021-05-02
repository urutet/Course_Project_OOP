using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.MVVM.Model
{
    public class Shop
    {
        public string Shop_ID { get; set; }
        public Address address { get; set; }
        public string Phone_Num { get; set; }

        public Shop()
        {

        }

        public Shop(string shop_ID, Address address, string phone_Num)
        {
            Shop_ID = shop_ID;
            this.address = address;
            Phone_Num = phone_Num;
        }
    }
}
