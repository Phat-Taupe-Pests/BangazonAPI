using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Data;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Written by Ben Greaves

namespace BangazonAPI.Controllers
{
    [Route("[controller]")]
    public class PaymentTypeController : Controller
    {
        private BangazonAPIContext _context;
        public PaymentTypeController(BangazonAPIContext ctx)
        {
            _context = ctx;
        }

        // Get All payment type
        // ex. url/paymenttype
        // returns all payment types if any exist

        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> paymentTypes = from paymentType in _context.PaymentType select paymentType;

            if (paymentTypes == null)
            {
                return NotFound();
            }

            return Ok(paymentTypes);

        }

        // Get a single payment type by PaymentTypeID
        // ex. paymenttype/4 etc.
        [HttpGet("{id}", Name = "GetSinglePaymentType")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PaymentType paymentType = _context.PaymentType.Single(m => m.PaymentTypeID == id);

                if (paymentType == null)
                {
                    return NotFound();
                }
                
                return Ok(paymentType);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        // Post a new payment type
        // ex. /paymenttype
        // Requires an Object:
        // {
        //     "Name": "Example",
        //     "AccountNumber": ex. 1123,
        //     "CustomerID": ex. 1
        // }

        [HttpPost]
        public IActionResult Post([FromBody] PaymentType newPaymentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PaymentType.Add(newPaymentType);
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PaymentTypeExists(newPaymentType.PaymentTypeID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetSinglePaymentType", new { id = newPaymentType.PaymentTypeID }, newPaymentType);
        }

        private bool PaymentTypeExists(int paymentTypeID)
        {
          return _context.PaymentType.Count(e => e.PaymentTypeID == paymentTypeID) > 0;
        }

        // Edit a payment type
        // ex. /paymenttype/4
        // Requires an Object:
        // {
        //     "PaymentTypeID": ex. 1,
        //     "AccountNumber": ex. 2,
        //     "CustomerID": ex. 3,
        //     "Name": "Example"
        // }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PaymentType modifiedPaymentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modifiedPaymentType.PaymentTypeID)
            {
                return BadRequest();
            }

            _context.Entry(modifiedPaymentType).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTypeExists(id))
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

        // Deletes a payment type
        // ex. paymenttype/4
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PaymentType paymentType = _context.PaymentType.Single(m => m.PaymentTypeID == id);
            if (paymentType == null)
            {
                return NotFound();
            }

            _context.PaymentType.Remove(paymentType);
            _context.SaveChanges();

            return Ok(paymentType);
        }
    }
}