using System;
using System.Collections.Generic;
using System.Text;
using JParts.Enums;

namespace JParts.MVVM.Model
{
    public class Order
    {
        public string Order_ID { get; set; }
        public Client Client_ID { get; set; }
        public List<Part> OrderedParts { get; set; }
        public Shop shop { get; set; }
        public double Price { get; set; }
        public StatusEnum Status { get; set; }
    }
}
