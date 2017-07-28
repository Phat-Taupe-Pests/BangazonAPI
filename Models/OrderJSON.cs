using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//Written By Ben Greaves
namespace BangazonAPI.Models
{
    // A Class that structures orders as JSON
    // Returns JSON in an easily readable format, with products attached to the order and their quantity
    // Contains OrderID, CustomerID, PaymentTypeID, and an array of Products
    public class OrderJSON
    {
        public int OrderID {get; set;}
        public int CustomerID {get; set;}
        public int? PaymentTypeID {get; set;}
        public virtual ICollection<ProductJSON> Products {get; set;}

    }
}