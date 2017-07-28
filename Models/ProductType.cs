using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

// Written by: Matt Augsburger
namespace BangazonAPI.Models
{
    // Contains the types of products offered by Bangazon
    // Contains Name and a collection of products are of this product type
    // Class is Public. ProductTypeID is the primary key
    public class ProductType
    {
        [Key]
        public int ProductTypeID {get; set;}

        [Required]
        public string Name {get; set;}

        ICollection<Product> Products;

    }
}