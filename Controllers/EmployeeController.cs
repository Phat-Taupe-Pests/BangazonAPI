using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Written by: Chaz Henricks
namespace BangazonAPI.Controllers
{
    //Sets URL route to <websitename>/Employee
    [Route("[controller]")]
    //Creates a new Employee controller class that inherits methods from AspNetCore Controller class
    public class EmployeeController : Controller
    {
        
        private BangazonAPIContext _context;
       
        public EmployeeController(BangazonAPIContext ctx)
        {
            _context = ctx;
        }

        // GET METHOD
        //http://localhost:5000/Employee/ will return a list of all employees. 
        //Takes no arguments
        [HttpGet]
        public IActionResult Get()
        {
            //Sets a new IQuerable Collection of <objects> that will be filled with each instance of _context.Employee
            IQueryable<object> employees = from employee in _context.Employee select employee;

            //if the collection is empty will retur NotFound and exit the method. 
            if (employees == null)
            {
                return NotFound();
            }

            //otherwise return list of the employees
            return Ok(employees);

        }

        // GET Single Employee
         //http://localhost:5000/Employee/{id} will return info on a single Employee based on ID 
         //Takes single ID as argument
        [HttpGet("{id}", Name = "GetSingleEmployee")]

        //will run Get based on the id from the url route. 
        public IActionResult Get([FromRoute] int id)
        {
            //if you request anything other than an Id you will get a return of BadRequest. 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //will search the _context.Employee for an entry that has the id we are looking for
                //if found, will return that Employee
                //if not found will return 404. 
                Employee employee = _context.Employee.Single(m => m.EmployeeID == id);

                if (employee == null)
                {
                    return NotFound();
                }
                
                return Ok(employee);
            }
            //if the try statement fails for some reason, will return error of what happened. 
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST
        //http://localhost:5000/Employee/ will post new Employee to the DB
        //requires {"Name": "string", "JobTitle": "string", "DepartmentID": validDeptIdInt} to post. 

        [HttpPost] 
        public IActionResult Post([FromBody] Employee newEmployee)
        {
            //Checks to make sure model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employee.Add(newEmployee);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {

                if (EmployeeExists(newEmployee.EmployeeID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            //if everything successfull, will run the "GetSingleEmployee" method while passing the new ID that was created and return the new Employee
            return CreatedAtRoute("GetSingleEmployee", new { id = newEmployee.EmployeeID }, newEmployee);
        }


        //Helper method to check to see if a EmployeeID is already in the system
        private bool EmployeeExists(int employeeId)
        {
          return _context.Employee.Count(e => e.EmployeeID == employeeId) > 0;
        }

        // PUT 
         //http://localhost:5000/Employee/{id} will edit a employee entry in the DB.  
         //Needs entire employee value set in order to change/update 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee modifiedEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modifiedEmployee.EmployeeID)
            {
                return BadRequest();
            }

            _context.Entry(modifiedEmployee).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }
    }
}