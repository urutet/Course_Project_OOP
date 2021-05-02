using System;
using System.Collections.Generic;
using System.Text;

namespace JParts.MVVM.Model
{
    public class Car
    {
        public string Car_ID { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public Car(string car_ID, string manufacturer, string model, int year)
        {
            Car_ID = car_ID;
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
        }

        public Car()
        {

        }
    }
}
