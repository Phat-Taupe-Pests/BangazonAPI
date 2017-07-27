using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Written by: Eliza Meeks
namespace BangazonAPI.Controllers
{
     //Sets URL route to <websitename>/TrainingProgram
    //Creates a new Customer controller class that inherits methods from AspNetCore Controller class
    [Route("[controller]")]
    public class TrainingProgramController : Controller
    {
        //Sets up an empty variable _context that will  be a reference of our BangazonAPIContext class
        private BangazonAPIContext _context;
        //Contructor that instantiates a new Customer controller and sets _context equal to a new instance of our BangazonAPIContext class
        public TrainingProgramController(BangazonAPIContext ctx)
        {
            _context = ctx;
        }

        // GET {url}/TrainingProgram will return a list of all training programs
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

        // GET {url}/TrainingProgram/{id} will get on trianing program based on the ID argument input.
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

        // POST {url}/TrainingProgram to post a new training program. Use the following format to POST the object.
        //{"DateStart": "08-01-2017", "DateEnd": "08-04-2017", "MaxAttendees": 10}
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
                if (TrainingProgramExists(newTrainingProgram.TrainingProgramID))
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
        // Checks to see if the trianing program exists before posting a new one.
        private bool TrainingProgramExists(int trainingProgramID)
        {
          return _context.TrainingProgram.Count(e => e.TrainingProgramID == trainingProgramID) > 0;
        }

        // PUT {url}/TrainingProgram/5 edits your database.
        // Use this format to PUT. {"DateStart": "08-01-2017", "DateEnd": "08-04-2017", "MaxAttendees": 10}
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
                if (!TrainingProgramExists(id))
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

        // DELETE {url}/TrainingProgram/5 Deletes something based on an id as long as the start date is in the future.
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
            // Creates a DateTime variable for today.
            DateTime thisDay = DateTime.Today;
            // Compares today with the date the training program starts.
            var compare = DateTime.Compare(thisDay, (DateTime)singleTrainingProgram.DateStart);
            // Only deletes the program if it hasn't started yet, else it returns a bad request.
            if (compare < 0)
            {
                _context.TrainingProgram.Remove(singleTrainingProgram);
                _context.SaveChanges();

                return Ok(singleTrainingProgram);
            } else {
                return BadRequest();
            }

        }

    }
}