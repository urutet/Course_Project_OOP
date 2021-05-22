using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.MVVM.Model
{
    public class CartPart
    {
        public Part Part { get; set; }

        public int? Amount { get; set; }

        public CartPart()
        {

        }

        public CartPart(Part part, int? amount)
        {
            Part = part;
            Amount = amount;
        }
    }
}
