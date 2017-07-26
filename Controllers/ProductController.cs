using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Written by: Andrew Rock

namespace BangazonAPI.Controllers
{
    [Route("[controller]")]
    public class ProductController: Controller
    {
        private BangazonAPIContext _context;

        public ProductController(BangazonAPIContext ctx)
        {
            _context = ctx;
        }

//Get all Products from the DB
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<Product> products = from product in _context.Product select product;

            if(products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }
//Get Single Product from the DB
        [HttpGet("{id}", Name = "GetSingleProduct")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Product singleProduct = _context.Product.Single(m => m.ProductID == id);

                if (singleProduct == null)
                {
                    return NotFound();
                }
                
                return Ok(singleProduct);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }
//Posting a new product to the DB
        [HttpPost]
        public IActionResult Post([FromBody] Product newProduct)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Product.Add(newProduct);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(newProduct.ProductID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleProduct", new { id = newProduct.ProductID }, newProduct);
        }

//Editting an already Existing Product in the DB
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product modifiedProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modifiedProduct.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(modifiedProduct).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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


//Helper Function to check for existence of a Product
        private bool ProductExists(int productID)
        {
          return _context.Product.Count(e => e.ProductID == productID) > 0;
        }


//Delete a Product Listing from the DB
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = _context.Product.Single(m => m.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}