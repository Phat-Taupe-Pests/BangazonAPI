using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//Written By: Andrew Rock 
namespace BangazonAPI.Models
{
    // Contains information abou the departments in Bangazon
    // Includes Name, ExpenseBudget, and a collection of Employees who work in that department
    // CustomerID is the Primary Key

    public class Department
    {
        [Key]
        public int DepartmentID {get; set;}

        [Required]
        public string Name {get; set;}
        [Required]
        public double ExpenseBudget {get; set;}

        //Foreign Key Relationship with Employees 
        ICollection<Employee> Employees;
        
    }
}