using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
{
    // Class Created by MEA
    // Class holds ProdctTypeID and A Name
    // Class is Public
    public class ProductType
    {
        [Key]
        public int ProductTypeID {get; set;}

        [Required]
        public string Name {get; set;}

        // ICollection<Product> Products;

    }
}