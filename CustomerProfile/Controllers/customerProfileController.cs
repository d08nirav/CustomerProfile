using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerProfile.Data;
using CustomerProfile.Models;

namespace CustomerProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class customerProfileController : ControllerBase
    {
        private readonly CustomerProfileDbContext _context;

        public customerProfileController(CustomerProfileDbContext context)
        {
            _context = context;
        }

        // GET: api/customerProfile
        [HttpGet]
        public IEnumerable<customerprofile> Getcustomerprofile()
        {
            return _context.customerprofile;
        }

        // GET: api/customerProfile/5
        /* [HttpGet("{id}")]
         public async Task<IActionResult> Getcustomerprofile([FromRoute] string id)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             var customerprofile = await _context.customerprofile.FindAsync(id);

             if (customerprofile == null)
             {
                 return NotFound();
             }

             return Ok(customerprofile);
         }*/

        // GET: api/customerProfile/Nirav
        [HttpGet("{name}")]
        public IEnumerable<customerprofile> GetcustomerprofilebyName([FromRoute] string name)
        {
            return _context.customerprofile.Where(s => s.Name == name);
        }

        // PUT: api/customerProfile/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id,[FromBody]customerprofile customerprofile)
        {
            var customerprofileTuple = await _context.customerprofile.SingleOrDefaultAsync(m => m.CustomerProfileID == id);
            if (customerprofileTuple == null)
            {
                return NotFound();
            }
            customerprofileTuple.Name = customerprofile.Name;
            customerprofileTuple.Address = customerprofile.Address;
            customerprofileTuple.PhoneNumber = customerprofile.PhoneNumber;
            customerprofileTuple.DateOfBirth = customerprofile.DateOfBirth;
            _context.Update(customerprofileTuple);
            _context.SaveChanges();
            return Ok(customerprofile);
        }

        /*public async Task<IActionResult> Putcustomerprofile([FromRoute] string id, [FromBody] customerprofile customerprofile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerprofile.Name)
            {
                return BadRequest();
            }

            _context.Entry(customerprofile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customerprofileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/customerProfile
        [HttpPost]
        public async Task<IActionResult> Postcustomerprofile([FromBody] customerprofile customerprofile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.customerprofile.Add(customerprofile);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (customerprofileExists(customerprofile.Name))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("Getcustomerprofile", new { id = customerprofile.Name }, customerprofile);
        }

        // DELETE: api/customerProfile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecustomerprofile([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerprofile = await _context.customerprofile.FindAsync(id);
            if (customerprofile == null)
            {
                return NotFound();
            }

            _context.customerprofile.Remove(customerprofile);
            await _context.SaveChangesAsync();

            return Ok(customerprofile);
        }

        private bool customerprofileExists(string id)
        {
            return _context.customerprofile.Any(e => e.Name == id);
        }
    }
}