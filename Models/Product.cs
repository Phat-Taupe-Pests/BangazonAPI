using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//Written by: Andrew Rock

namespace BangazonAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductID {get; set;}

        [Required]
        public string Title {get; set;}

        [Required]
        public string Description {get; set;}

        [Required]
        public double Price {get; set;}

        public int ProductTypeID {get; set;}
        public ProductType ProductType {get; set;}

        public int CustomerID {get; set;}
        public Customer Customer {get; set;}

        public ICollection<ProductOrder> ProductOrders {get; set;}
    }

}