using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//Written By: Chaz Henricks
namespace BangazonAPI.Models
{
    // Contains information on employees of Bangazon
    // Includes Name, JobTitle, DateStarted, Department (FK ID),  and IsSupervisor (are they head of their department? bool set to 0 by default)
    // EmployeeID is the Primary Key

    public class Employee
    {
        [Key]
        public int EmployeeID {get; set;}

        [Required]
        public string Name {get; set;}
        [Required]
        public string JobTitle {get; set;}
        [Required]
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