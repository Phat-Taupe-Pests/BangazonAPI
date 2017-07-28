using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//Written by: Eliza Meeks
namespace BangazonAPI.Models
{
    // Contains information about the employee training programs at Banagazon
    // Includes Name, DateStart, DateEnd, and MaxAttendees
    // TrainingProgramID is the Primary Key
    public class TrainingProgram
    {
        // Database structure for our Trianing Programs.
        [Key]
        public int TrainingProgramID {get; set;}

        [Required] //Added by Ben Greaves
        public string Name {get; set;}

        [Required]
        public DateTime? DateStart {get; set;}

        [Required]
        public DateTime? DateEnd {get; set;}

        [Required]        
        public int? MaxAttendees {get; set;}
    }
}