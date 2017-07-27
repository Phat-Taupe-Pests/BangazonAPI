using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Written by: Andrew Rock

namespace BangazonAPI.Models
{
    public class ProductOrder
    {
        [Required]
        public int ProductID {get; set;}
        public Product Product {get; set;}
        [Required]
        public int OrderID {get; set;}
        public Order Order {get; set;}
    }

}