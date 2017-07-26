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
    // Controller created by MEA
    [Route("[controller]")]
    public class ComputerController : Controller
    {
        private BangazonAPIContext _context;
        public ComputerController(BangazonAPIContext ctx)
        {
            _context = ctx;
        }

        // GET url/Computer
        // Returns List of Computers if any exist
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> computers = from computer in _context.Computer select computer;

            if (computers == null)
            {
                return NotFound();
            }

            return Ok(computers);

        }

        // GET url/Computer/{id}
        // Returns a Specific Computer correlating to id

        [HttpGet("{id}", Name = "GetSingleComputer")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Computer computer = _context.Computer.Single(m => m.ComputerID == id);

                if (computer == null)
                {
                    return NotFound();
                }
                
                return Ok(computer);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST url/Computer
        // Creates a Computer in the database

        [HttpPost]
        public IActionResult Post([FromBody] Computer newComputer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Computer.Add(newComputer);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ComputerExists(newComputer.ComputerID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleComputer", new { id = newComputer.ComputerID }, newComputer);
        }

        private bool ComputerExists(int computerID)
        {
          return _context.Computer.Count(e => e.ComputerID == computerID) > 0;
        }
        // PUT url/Computer/{id}
        // Edits a Specific Computer from the database
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Computer modifiedComputer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modifiedComputer.ComputerID)
            {
                return BadRequest();
            }

            _context.Entry(modifiedComputer).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerExists(id))
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
        // DELETE url/Computer/{id}
        // Removes a specific Computer from the database
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Computer computer = _context.Computer.Single(m => m.ComputerID == id);
            if (computer == null)
            {
                return NotFound();
            }

            _context.Computer.Remove(computer);
            _context.SaveChanges();

            return Ok(computer);
        }

    }
}