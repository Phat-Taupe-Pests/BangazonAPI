using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//Written By Ben Greaves
namespace BangazonAPI.Models
{
    // A Class that structures an Order as JSON
    // Returns JSON in an easily readable format that can be added to the ICollection "Orders" on the CustomerWithOrderJSON Class
    // Contains OrderID, CustomerID, PaymentTypeID, DateCreated
    public class OrderOnCustomerJSON
    {
        public int OrderID {get; set;}
        public int CustomerID {get; set;}
        public int? PaymentTypeID {get; set;}
        public DateTime DateCreated {get; set;}
    }
}