using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JParts.MVVM.Model
{
    public class Car
    {
        [Key]
        public string CarID { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public Car(string car_ID, string manufacturer, string model, int year)
        {
            CarID = car_ID;
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
        }

        public Car()
        {

        }
    }
}
