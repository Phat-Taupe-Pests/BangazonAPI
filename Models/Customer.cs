using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//Written by the entire team. 
namespace BangazonAPI.Models
{
    // Contains information about our customers (vendors and purchasers)
    // Includes FirstName, LastName, DateCreated, DateLastInteraction (when did they last log in or make a transaction?),
    // IsActive (bool to track whether they are active members as of 180 days ago), a collection of products they have created,
    // a collection of orders they have opened and placed, and a list of payment types that they can use to pay for orders.

    // CustomerID is the Primary Key
    public class Customer
    {
        [Key]
        public int CustomerID {get; set;}

        [Required]
        public string FirstName {get; set;}
        [Required]
        public string LastName {get; set;}
        public DateTime? DateCreated {get; set;}
        public DateTime? DateLastInteraction {get; set;}
        [Required]
        public int IsActive {get; set;}

        //To change the default for InActive to 1, need to set it in a Constructor
        public Customer()
        {
            IsActive = 1;
        }

        ICollection<Product> Products;
        // Setting the Foreign Key relationship with Orders --Eliza
        ICollection<Order> Orders;
        ICollection<PaymentType> PaymentTypes;

    }
}