using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//Written by: Ben Greaves

namespace BangazonAPI.Models
{
    // A Class designed to return JSON in a neat format
    public class ProductJSON
    {
        [Required]
        public int ProductID {get; set;}

        [Required]
        public string Name {get; set;}

        [Required]
        public double Price {get; set;}

        [Required]
        public int Quantity {get; set;}

    }

}