using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


//Written By: Andrew Rock. 
namespace BangazonAPI.Models
{
    //Creates a new Department class
    //CustomerID is the Primary Key

    public class Department
    {
        [Key]
        public int DepartmentID {get; set;}

        [Required]
        public string Name {get; set;}
        [Required]
        public double ExpenseBudget {get; set;}

        //Foreign Key Relationship with Employees 
        //ICollection<Employee> Employees;
        
    }
}