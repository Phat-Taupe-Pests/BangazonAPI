using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BangazonAPI.Controllers
{
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private BangazonAPIContext _context;
        public CustomerController(BangazonAPIContext ctx)
        {
            _context = ctx;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> customers = from customer in _context.Customer select customer;

            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);

        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetSingleCustomer")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Customer customer = _context.Customer.Single(m => m.CustomerID == id);

                if (customer == null)
                {
                    return NotFound();
                }
                
                return Ok(customer);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Customer newCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customer.Add(newCustomer);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(newCustomer.CustomerID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleCustomer", new { id = newCustomer.CustomerID }, newCustomer);
        }

        private bool CustomerExists(int customerId)
        {
          return _context.Customer.Count(e => e.CustomerID == customerId) > 0;
        }

        // PUT api/values/5
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