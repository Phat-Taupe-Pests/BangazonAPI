using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int AccountNumber {get; set; }
        
        [Required]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        // Setting foreign Key relationship with orders --Eliza
        ICollection<Order> Orders;
    }
}