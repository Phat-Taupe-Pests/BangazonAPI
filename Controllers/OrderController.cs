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
    public class OrderController : Controller
    {
        private BangazonAPIContext _context;
        public OrderController(BangazonAPIContext ctx)
        {
            _context = ctx;
        }

        // GET url/Order
        // Gets a list of all orders -- Eliza
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> orders = from order in _context.Order select order;

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);

        }

        // GET url/Order/{id}
        // Gets one order based on an id -- Eliza
        [HttpGet("{id}", Name = "GetSingleOrder")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Order order = _context.Order.Single(m => m.OrderID == id);

                if (order == null)
                {
                    return NotFound();
                }
                
                return Ok(order);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST url/Order
        // Posts a new order -- Eliza
        [HttpPost]
        public IActionResult Post([FromBody] Order newOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Order.Add(newOrder);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(newOrder.OrderID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleCustomer", new { id = newOrder.OrderID }, newOrder);
        }

        private bool CustomerExists(int orderID)
        {
          return _context.Order.Count(e => e.OrderID == orderID) > 0;
        }

        // PUT url/Order/5
        // Edits something in the database; you must send the ENTIRE object up. -- Eliza
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Order modifiedOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modifiedOrder.OrderID)
            {
                return BadRequest();
            }

            _context.Entry(modifiedOrder).State = EntityState.Modified;

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

        // DELETE url/Order/5
        // Deletes something based on an id. -- Eliza
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order singleOrder = _context.Order.Single(m => m.OrderID == id);
            if (singleOrder == null)
            {
                return NotFound();
            }

            _context.Order.Remove(singleOrder);
            _context.SaveChanges();

            return Ok(singleOrder);
        }

    }
}