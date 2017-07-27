using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Written by: Andrew Rock

namespace BangazonAPI.Models
{
    public class ProductOrder
    {
        [Key]
        public int ProductOrderID {get; set;}
        [Required]
        public int ProductID {get; set;}
        [Required]
        public int OrderID {get; set;}
        public virtual Product Product {get; set;}
        public virtual Order Order {get; set;}
    }

}