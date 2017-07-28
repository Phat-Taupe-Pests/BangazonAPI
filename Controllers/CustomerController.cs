using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Written by: Chaz Henricks, Eliza Meeks, Andrew Rock, Matt Augsburger, Ben Greaves
//all code written as a team unless stated otherwise
namespace BangazonAPI.Controllers
{
    //Sets URL route to <websitename>/Customer
    [Route("[controller]")]
    //Creates a new Customer controller class that inherits methods from AspNetCore Controller class
    public class CustomerController : Controller
    {
        //Sets up an empty variable _context that will  be a reference of our BangazonAPIContext class
        private BangazonAPIContext _context;
        //Contructor that instantiates a new Customer controller and sets _context equal to a new instance of our BangazonAPIContext class
        public CustomerController(BangazonAPIContext ctx)
        {
            _context = ctx;
        }

        // GET METHOD
        //http://localhost:5000/Customer/ will return a list of all customers. 
        [HttpGet]

        //Get() is a mathod from the AspNetCore Controller class to retreive info from database. 
        public IActionResult Get()
        {
            //Sets a new IQuerable Collection of <objects> that will be filled with each instance of _context.Customer
            IQueryable<object> customers = from customer in _context.Customer select customer;

            //if the collection is empty will retur NotFound and exit the method. 
            if (customers == null)
            {
                return NotFound();
            }

            //otherwise return list of the customers
            return Ok(customers);

        }

        // GET Single Customer
         //http://localhost:5000/Customer/{id} will return info on a single customer based on ID 
        [HttpGet("{id}", Name = "GetSingleCustomer")]

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
                //will search the _context.Customer for an entry that has the id we are looking for
                //if found, will return that customer
                //if not found will return 404. 
                Customer customer = _context.Customer.Single(m => m.CustomerID == id);

                if (customer == null)
                {
                    return NotFound();
                }
                
                return Ok(customer);
            }
            //if the try statement fails for some reason, will return error of what happened. 
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST
        // //http://localhost:5000/Customer/ will post new customer to the DB 
        [HttpPost]
        //takes the format of Customer type as a JSON format and adds to database. 
        public IActionResult Post([FromBody] Customer newCustomer)
        {
            //Checks to make sure model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Will add new customer to the context
            //This will not yet be added to DB until .SaveChanges() is run
            _context.Customer.Add(newCustomer);
            

            //Will attempt to save the changes to the DB.
            //If there is an error, will throw exception code. 
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                //this checks to see if a new customer we are trying to add has a CustomerID that already exists in the system
                if (CustomerExists(newCustomer.CustomerID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            //if everything successfull, will run the "GetSingleCustomer" method while passing the new ID that was created and return the new Customer
            return CreatedAtRoute("GetSingleCustomer", new { id = newCustomer.CustomerID }, newCustomer);
        }


        //Helper method to check to see if a CustomerID is already in the system
        private bool CustomerExists(int customerId)
        {
          return _context.Customer.Count(e => e.CustomerID == customerId) > 0;
        }

        // PUT 
         //http://localhost:5000/Customer/{id} will edit a customer entry in the DB.  
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer modifiedCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modifiedCustomer.CustomerID)
            {
                return BadRequest();
            }

            _context.Entry(modifiedCustomer).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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