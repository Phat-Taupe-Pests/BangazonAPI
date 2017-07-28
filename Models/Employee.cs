using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


//Written By: Chaz Henricks
namespace BangazonAPI.Models
{
    //Creates a new Employee class
    //EmployeeID is the Primary Key

    public class Employee
    {
        [Key]
        public int EmployeeID {get; set;}

        [Required]
        public string Name {get; set;}
        [Required]
        public string JobTitle {get; set;}
     
        public DateTime? DateStarted {get; set;}

        [Required]
        public int IsSupervisor {get; set;}

        public int DepartmentID {get; set;}

        public Department Department {get; set;}

        public Employee()
        {
            IsSupervisor = 0;
        }




    }
}