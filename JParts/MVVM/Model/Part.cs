using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace JParts.MVVM.Model
{
    public class Part
    {
        public string Part_ID { get; set; }
        public Car Car_ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public bool Availability { get; set; }
        public ImageSource Image { get; set; }

        public Part()
        {

        }

        public Part(string part_ID, Car car_ID, string name, string type, double price, bool availability, ImageSource image)
        {
            Part_ID = part_ID;
            Car_ID = car_ID;
            Name = name;
            Type = type;
            Price = price;
            Availability = availability;
            Image = image;
        }
    }
}
