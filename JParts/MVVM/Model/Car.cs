using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JParts.MVVM.Model
{
    public class Car
    {
        [Key]
        public int CarID { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public List<Part> Parts { get; set; }

        public Car(string manufacturer, string model, int? year)
        {
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
            Parts = new List<Part>();
        }

        public Car()
        {

        }
    }
}
