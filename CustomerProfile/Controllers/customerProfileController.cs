using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerProfile.Data;
using CustomerProfile.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        // GET: api/customerProfile/Nirav
        [HttpGet("{name}")]
        public IEnumerable<customerprofile> GetcustomerprofilebyName([FromRoute] string name)
        {
            return _context.customerprofile.Where(s => s.Name == name);
        }

        // PUT: api/customerProfile/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id,[FromBody]customerprofileTemp customerprofile)
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

        /// <summary>
        /// Creates a New Customer.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Postcustomerprofile([FromBody] customerprofileTemp customerprofile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            customerprofile profile = new customerprofile();
            profile.Name = customerprofile.Name;
            profile.Address = customerprofile.Address;
            profile.DateOfBirth = customerprofile.DateOfBirth;
            profile.PhoneNumber = customerprofile.PhoneNumber;
            _context.customerprofile.Add(profile);
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

        private bool customerprofileExists(string id)
        {
            return _context.customerprofile.Any(e => e.Name == id);
        }
    }

    public class customerprofileTemp
    {
        
        public string Name { get; set; }
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        //[DisplayFormat( DataFormatString = "{0:d}")]
        public DateTime DateOfBirth { get; set; }
    }
}