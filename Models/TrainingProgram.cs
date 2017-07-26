using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
{
    public class TrainingProgram
    {
        // Database structure for our Trianing Programs. -- Eliza
        [Key]
        public int TrainingProgramID {get; set;}

        [Required]
        public DateTime DateStarted {get; set;}

        [Required]
        public DateTime DateEnded {get; set;}

        [Required]        
        public int MaxAttendees {get; set;}


    }
}