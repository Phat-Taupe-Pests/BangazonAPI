using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//Written By: MEA 
namespace BangazonAPI.Models
{
    // Contains information about the computers used at Banagazon
    // Includes the DatePurchased and the DateDecomissioned
    // ComputerID is the Primary Key
    public class Computer
    {
        [Key]
        public int ComputerID {get; set;}

        [Required]
        public DateTime? DatePurchased {get; set;}

        public DateTime? DateDecomissioned {get; set;}

    }
}