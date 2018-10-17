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
        [HttpGet("{id}")]
        public async Task<IActionResult> Getcustomerprofile([FromRoute] int id)
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
        }

        // PUT: api/customerProfile/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcustomerprofile([FromRoute] int id, [FromBody] customerprofile customerprofile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerprofile.CustomerProfileID)
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
        }

        // POST: api/customerProfile
        [HttpPost]
        public async Task<IActionResult> Postcustomerprofile([FromBody] customerprofile customerprofile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.customerprofile.Add(customerprofile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcustomerprofile", new { id = customerprofile.CustomerProfileID }, customerprofile);
        }

        // DELETE: api/customerProfile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecustomerprofile([FromRoute] int id)
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

        private bool customerprofileExists(int id)
        {
            return _context.customerprofile.Any(e => e.CustomerProfileID == id);
        }
    }
}