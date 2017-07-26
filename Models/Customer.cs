using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


//Written By: Pair Programmed as a Team. 
namespace BangazonAPI.Models
{
    //Creates a new Customer class
    //CustomerID is the Primary Key

    public class Customer
    {
        [Key]
        public int CustomerID {get; set;}

        [Required]
        public string FirstName {get; set;}
        [Required]
        public string LastName {get; set;}
        [Required]
        public DateTime DateCreated {get; set;}
        [Required]
        public DateTime DateLastInteraction {get; set;}

        [Required]
        public int IsActive {get; set;}

        //To change the default for InActive to 1, need to set it in a Constructor
        public Customer()
        {
            IsActive = 1;
        }

        // ICollection<Product> Products;
        ICollection<PaymentType> PaymentTypes;
        // ICollection<Order> Orders;


    }
}