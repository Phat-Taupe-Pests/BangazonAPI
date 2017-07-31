using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Written by: Andrew Rock
namespace BangazonAPI.Controllers
{
    //Sets URL route to <websitename>/department
    [Route("[controller]")]
    [EnableCors("AllowOnlyBangazonians")]
    //Creates a new Department controller class that inherits methods from AspNetCore Controller class
    public class DepartmentController : Controller
    {
        //Sets up an empty variable _context that will  be a reference of our BangazonAPIContext class
        private BangazonAPIContext _context;
        //Contructor that instantiates a new Department controller and sets _context equal to a new instance of our BangazonAPIContext class
        public DepartmentController(BangazonAPIContext ctx)
        {
            _context = ctx;
        }

        // GET METHOD
        //http://localhost:5000/department/ will return a list of all Departments. 
        [HttpGet]

        //Get() is a mathod from the AspNetCore Controller class to retreive info from database. 
        public IActionResult Get()
        {
            //Sets a new IQuerable Collection of <objects> that will be filled with each instance of _context.Department
            IQueryable<Department> Departments = from department in _context.Department select department;

            //if the collection is empty will retur NotFound and exit the method. 
            if (Departments == null)
            {
                return NotFound();
            }

            //otherwise return list of the Departments
            return Ok(Departments);

        }

        // GET Single Department
         //http://localhost:5000/Department/{id} will return info on a single Department based on ID 
        [HttpGet("{id}", Name = "GetSingleDepartment")]

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
                //will search the _context.Department for an entry that has the id we are looking for
                //if found, will return that Department
                //if not found will return 404. 
                Department singleDepartment = _context.Department.Single(m => m.DepartmentID == id);

                if (singleDepartment == null)
                {
                    return NotFound();
                }
                
                return Ok(singleDepartment);
            }
            //if the try statement fails for some reason, will return error of what happened. 
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST
        // //http://localhost:5000/Department/ will post new Department to the DB
        // Requires an Object: {"Name": "theName", "DepartmentBudget": ex. 99999}
        [HttpPost]
        //takes the format of Department type as a JSON format and adds to database. 
        public IActionResult Post([FromBody] Department newDepartment)
        {
            //Checks to make sure model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Will add new Department to the context
            //This will not yet be added to DB until .SaveChanges() is run
            _context.Department.Add(newDepartment);
            

            //Will attempt to save the changes to the DB.
            //If there is an error, will throw exception code. 
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                //this checks to see if a new Department we are trying to add has a DepartmentID that already exists in the system
                if (DepartmentExists(newDepartment.DepartmentID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            //if everything successfull, will run the "GetSingleDepartment" method while passing the new ID that was created and return the new Department
            return CreatedAtRoute("GetSingleDepartment", new { id = newDepartment.DepartmentID }, newDepartment);
        }


        //Helper method to check to see if a DepartmentID is already in the system
        private bool DepartmentExists(int departmentID)
        {
          return _context.Department.Count(e => e.DepartmentID == departmentID) > 0;
        }

        // PUT 
         //http://localhost:5000/Department/{id} will edit a Department entry in the DB.  
        // Requires an Object: 
        // {
        //     "departmentID": 1,
        //     "name": "theName",
        //     "expenseBudget": 99999
        // }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Department modifiedDepartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modifiedDepartment.DepartmentID)
            {
                return BadRequest();
            }

            _context.Entry(modifiedDepartment).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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