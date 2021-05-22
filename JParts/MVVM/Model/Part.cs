using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Windows.Media;

namespace JParts.MVVM.Model
{
    public class Part
    {
        public int PartID { get; set; }

        public int CarID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double? Price { get; set; }
        public bool Availability { get; set; }
        public string Image { get; set; }
        public int? Amount { get; set; }

        public List<PartsOrders> PartsOrders { get; set; }

        public Part()
        {

        }

        public Part(int car_ID, string name, string type, double? price, bool availability, string image, int? amount)
        {
            CarID = car_ID;
            Name = name;
            Type = type;
            Price = price;
            Availability = availability;
            Image = image;
            Amount = amount;
        }
    }
}
