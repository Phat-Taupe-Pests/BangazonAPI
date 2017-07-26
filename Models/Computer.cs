using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


//Written By: Pair Programmed as a Team. 
namespace BangazonAPI.Models
{
    //Creates a new Computer class
    //ComputerID is the Primary Key

    public class Computer
    {
        [Key]
        public int ComputerID {get; set;}

        [Required]
        public DateTime DatePurchased {get; set;}

        public DateTime DateDecomissioned {get; set;}

    }
}