using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using JParts.Enums;

namespace JParts.MVVM.Model
{
    public class Order
    {

        [Key]
        public int OrderID { get; set; }
        public Client ClientID { get; set; }
        public List<Part> OrderedParts { get; set; }

        [ForeignKey("AddressOf")]
        public int AddressID { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }        //0 - in process, 1 - delivered
        public DateTime OrderDate { get; set; }

        public Address AddressOf { get; set; }

        public Order(Client clientID, List<Part> orderedParts, int addressID, decimal price, bool status, DateTime orderDate, Address addressOf)
        {
            ClientID = clientID;
            OrderedParts = orderedParts;
            AddressID = addressID;
            Price = price;
            Status = status;
            OrderDate = orderDate;
            AddressOf = addressOf;
        }
    }
}
