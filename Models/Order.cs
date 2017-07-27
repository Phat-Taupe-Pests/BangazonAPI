using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
{
    public class Order
    {
        // Database structure for our orders. -- Eliza
        [Key]
        public int OrderID {get; set;}

        [Required]
        public DateTime DateCreated {get; set;}
        
        public int CustomerID {get; set;}
        // Setting foreign key relationship with Customer --Eliza
        public Customer Customer {get; set;}
        public int PaymentTypeID {get; set;}
        public PaymentType PaymentType {get; set;}

        // ICollection<ProductOrder> ProductOrders;

        public virtual ICollection<ProductOrder> ProductOrders {get; set;}

    }
}