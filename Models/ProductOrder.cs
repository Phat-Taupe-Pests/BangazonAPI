using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//Written by: Andrew Rock

namespace BangazonAPI.Models
{
    public class ProductOrder
    {
        [Key]
        public int ProductOrderID {get; set;}

        public int ProductID {get; set;}
        public Product Product {get; set;}

        public int OrderID {get; set;}
        public Order Order {get; set;}
    }

}