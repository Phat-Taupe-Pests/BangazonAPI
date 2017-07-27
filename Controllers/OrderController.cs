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
            IQueryable<object> orders = _context.Order.Include("ProductOrders.Product");

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
                // ICollection<Product> productsList = new List<Product>();
                // Order order = _context.Order.Include("Products").Single(m => m.OrderID == id);
                Order order = _context.Order.Include("ProductOrders.Product").Single(m => m.OrderID == id);

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

        [HttpPost]
        public IActionResult Post([FromBody] Product addProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Order newOrder = new Order()
            {
                CustomerID = addProduct.CustomerID,
                PaymentTypeID = 1
            };
            _context.Order.Add(newOrder);
            ProductOrder newProductOrder = new ProductOrder()
            {
                OrderID = newOrder.OrderID,
                ProductID = addProduct.ProductID
            };
            _context.ProductOrder.Add(newProductOrder);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(newOrder.OrderID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleOrder", new { id = newOrder.OrderID }, newOrder);
        }

        private bool OrderExists(int orderID)
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
                if (!OrderExists(id))
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