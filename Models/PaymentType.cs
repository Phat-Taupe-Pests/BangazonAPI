using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

// Written by: Ben Greaves
namespace BangazonAPI.Models
{

    public class PaymentType
    {
    // Contains a payment type for a customer. A customer can have multiple payment types
    // Includes a Name, AccountNumber, customer (FK ID), and a collection of orders that used this payment method
        [Key]
        public int PaymentTypeID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int AccountNumber {get; set; }
        
        [Required]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        ICollection<Order> Orders;
    }
}