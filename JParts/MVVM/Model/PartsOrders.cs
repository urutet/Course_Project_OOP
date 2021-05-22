using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.MVVM.Model
{
    public class PartsOrders
    {
        public int PartID { get; set; }
        public Part Part { get; set; }

        public int OrderID { get; set; }
        public Order Order { get; set; }

        public int? Amount { get; set; }
    }
}
