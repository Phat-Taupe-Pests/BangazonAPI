using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
{
    // Created by Ben
    public class PaymentType
    {
    //Creates a new PaymentType class
    //PaymentTypeID is the Primary Key
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