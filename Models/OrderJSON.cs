using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//Written By Ben Greaves
namespace BangazonAPI.Models
{
    // A Class to structure the JSON easily so that Orders can be displayed with their products
    public class OrderJSON
    {
        public int OrderID {get; set;}
        public int CustomerID {get; set;}
        public int? PaymentTypeID {get; set;}
        public virtual ICollection<ProductJSON> Products {get; set;}

    }
}