using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
{
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

        public Customer()
        {
            IsActive = 1;
        }

        // ICollection<Product> Products;
        // ICollection<PaymentType> PaymentTypes;
        // ICollection<Order> Orders;


    }
}