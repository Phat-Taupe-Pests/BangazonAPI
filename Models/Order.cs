using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//Written by: Eliza Meeks
namespace BangazonAPI.Models
{
    // Database structure for our orders.
    // Order contains the DateCreated, Customer (FK ID), PaymentType (FK ID, null until order is complete),
    // and a collection of productOrders, join table entries representing products added to this order
    public class Order
    {
        
        [Key]
        public int OrderID {get; set;}

        [Required]
        public DateTime DateCreated {get; set;}
        
        public int CustomerID {get; set;}
        // Setting foreign key relationship with Customer --Eliza
        public Customer Customer {get; set;}
        public int? PaymentTypeID {get; set;}
        public PaymentType PaymentType {get; set;}

        // ICollection<ProductOrder> ProductOrders;

        public virtual ICollection<ProductOrder> ProductOrders {get; set;}

    }
}