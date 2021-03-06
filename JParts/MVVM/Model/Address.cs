using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JParts.MVVM.Model
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int? House_Num { get; set; }
        public int? Flat_Num { get; set; }

        public Address(string city, string street, int? house_Num, int? flat_Num)
        {
            City = city;
            Street = street;
            House_Num = house_Num;
            Flat_Num = flat_Num;
        }

        public Address()
        {

        }
    }
}
