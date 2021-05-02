using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.MVVM.Model
{
    public class Order
    {
        public string Order_ID { get; set; }
        public Client client { get; set; }
        public List<Part> OrderedParts { get; set; }
        public Shop shop { get; set; }
        public double Price { get; set; }
    }
}
