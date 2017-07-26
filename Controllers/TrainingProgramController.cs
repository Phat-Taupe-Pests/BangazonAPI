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
    public class TrainingProgramController : Controller
    {
        private BangazonAPIContext _context;
        public TrainingProgramController(BangazonAPIContext ctx)
        {
            _context = ctx;
        }

        // GET url/TrainingProgram
        // Gets a list of all training programs -- Eliza
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> trainingPrograms = from trainingProgram in _context.TrainingProgram select trainingProgram;

            if (trainingPrograms == null)
            {
                return NotFound();
            }

            return Ok(trainingPrograms);

        }

        // GET url/TrainingProgram/{id}
        // Gets one training program based on an id -- Eliza
        [HttpGet("{id}", Name = "GetSingleTrainingProgram")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                TrainingProgram trainingProgram = _context.TrainingProgram.Single(m => m.TrainingProgramID == id);

                if (trainingProgram == null)
                {
                    return NotFound();
                }
                
                return Ok(trainingProgram);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // POST url/TrainingProgram
        // Posts a new trainingprogram -- Eliza
        [HttpPost]
        public IActionResult Post([FromBody] TrainingProgram newTrainingProgram)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TrainingProgram.Add(newTrainingProgram);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(newTrainingProgram.TrainingProgramID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSingleCustomer", new { id = newTrainingProgram.TrainingProgramID }, newTrainingProgram);
        }

        private bool CustomerExists(int trainingProgramID)
        {
          return _context.TrainingProgram.Count(e => e.TrainingProgramID == trainingProgramID) > 0;
        }

        // PUT url/TrainingProgram/5
        // Edits something in the database; you must send the ENTIRE object up. -- Eliza
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TrainingProgram modifiedTrainingProgram)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modifiedTrainingProgram.TrainingProgramID)
            {
                return BadRequest();
            }

            _context.Entry(modifiedTrainingProgram).State = EntityState.Modified;

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

        // DELETE url/TrainingProgram/5
        // Deletes something based on an id. -- Eliza
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TrainingProgram singleTrainingProgram = _context.TrainingProgram.Single(m => m.TrainingProgramID == id);
            if (singleTrainingProgram == null)
            {
                return NotFound();
            }

            _context.TrainingProgram.Remove(singleTrainingProgram);
            _context.SaveChanges();

            return Ok(singleTrainingProgram);
        }

    }
}