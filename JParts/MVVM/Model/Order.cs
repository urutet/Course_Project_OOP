using System;
using System.Collections.Generic;
using System.Text;
using JParts.Enums;

namespace JParts.MVVM.Model
{
    public class Order
    {
        public string OrderID { get; set; }
        public Client ClientID { get; set; }
        public List<Part> OrderedParts { get; set; }
        public string ShopID { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }        //0 - in process, 1 - delivered
        public DateTime OrderDate { get; set; }
    }
}
