using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Windows.Media;

namespace JParts.MVVM.Model
{
    public class Part
    {
        public string PartID { get; set; }

        [ForeignKey("CarOf")]
        public string CarID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public bool Availability { get; set; }
        //public ImageSource Image { get; set; }
        public byte[] Image { get; set; }
        public Car CarOf { get; set; }

        public Part()
        {

        }

        public Part(string part_ID, string car_ID, string name, string type, double price, bool availability, byte[] image)
        {
            PartID = part_ID;
            CarID = car_ID;
            Name = name;
            Type = type;
            Price = price;
            Availability = availability;
            Image = image;
        }

        public Part(string partID, string carID, string name, string type, double price, bool availability, byte[] image, Car carOf)
        {
            PartID = partID;
            CarID = carID;
            Name = name;
            Type = type;
            Price = price;
            Availability = availability;
            Image = image;
            CarOf = carOf;
        }
    }
}
