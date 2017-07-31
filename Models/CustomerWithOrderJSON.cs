using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//Written By Ben Greaves
namespace BangazonAPI.Models
{
    // A Class that structures a Customer as JSON
    // Returns JSON in an easily readable format, with completed orders attached to the customer
    // Contains CustomerID, FirstName, LastName, and an array of Orders that they have completed
    public class CustomerWithOrderJSON
    {
        public int CustomerID {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public virtual ICollection<OrderOnCustomerJSON> Orders {get; set;}

    }
}

