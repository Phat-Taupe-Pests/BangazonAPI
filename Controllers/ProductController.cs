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
    //Sets URL route to <websitename>/Products
    [Route("[controller]")]

    //Creates a new Product controller class that inherits methods from AspNetCore Controller class   
    public class ProductController: Controller
    {
        //Sets up an empty variable _context that will  be a reference of our BangazonAPIContext class
        private BangazonAPIContext _context;
        //Contructor that instantiates a new Product controller and sets _context equal to a new instance of our BangazonAPIContext class
        public ProductController(BangazonAPIContext ctx)
        {
            _context = ctx;
        }

        //GET all method
        //http://localhost:5000/product/ will return a list of all products. 
        [HttpGet]
        //Get() is a method from the AspNetCore Controller class to retreive info from database. 
        public IActionResult Get()
        {
            IQueryable<Product> products = from product in _context.Product select product;

            if(products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }
        //Get Single Product from the DB method
        //http://localhost:5000/product/id - id is an integer that represents the unique id for each product in the product datatable
        //Examples {url}/product/1 or {url}/product/42
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
        //Post a new product tot the database's product table
        //Requires a stringified json object to be passed thro the http request that contains all the properties of the Product class
        //Property(type): Price(double) Title(string) Description(string) ProductType(int - foriegn key of ProductType) CustomerID(int- foriegn key from Customer)
        //Note: CustomerID is for the customer that created the Product
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

        //Put Method
        //Edits a preexisting Product in the database
        //Requires a compete Product class to be passed as a stringified JSON object 
        //Property(type): Price(double) Title(string) Description(string) ProductType(int - foriegn key of ProductType) CustomerID(int- foriegn key from Customer)
        //Note: CustomerID is for the customer that created the Product

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
        //Used by other methods in the class
        private bool ProductExists(int productID)
        {
          return _context.Product.Count(e => e.ProductID == productID) > 0;
        }


        //Delete a Product method
        //Requires the unique ID of the product you want to delete appended to the end of the url in the http request
        //http://localhost:5000/product/id - id is an integer that represents the unique id for each product in the product datatable
        //Examples {url}/product/1 or {url}/product/42

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