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
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using System.Net;

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
        /// <summary>
        /// Returns all the Customers in the Database.
        /// </summary>
        /// <returns>All Customers in the Database</returns>
        /// <response code="200">Returns Customer List</response>
        /// <response code="400">Bad Request</response> 
        [HttpGet]

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IEnumerable<customerprofile> Getcustomerprofile()
        {
            return _context.customerprofile;
        }

        /// <summary align="left">
        /// Searches a Customer in the database by keywork.
        /// </summary>
        /// <param name="keyword">Name, Address, Phone number or Date of Birth of the Customer.</param>
        /// <returns>One or more records based on the search keyword </returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="404">If no result found</response> 
        // GET: api/customerProfile/Nirav
        [HttpGet("{keyword}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetcustomerprofilebyName([FromRoute] string keyword)
        {
            var result = _context.customerprofile.Where(s => s.Name == keyword);
            if (result.Count() == 0)
            {
                result = _context.customerprofile.Where(s => s.Address == keyword);
                if (result.Count() == 0)
                {
                    result = _context.customerprofile.Where(s => s.PhoneNumber == keyword);
                    if (result.Count() == 0)
                    {
                        if (DateTime.TryParseExact(keyword, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime d))
                        {
                            result = _context.customerprofile.Where(s => s.DateOfBirth == d);
                        }
                    }
                }
            }
            Console.WriteLine("Nirav: Total number of results: " + result.Count());
            if (result.Count() == 0)
                return NotFound("No record exisits in the database with Name/Address/Phone Number/date of birth equal to: "+keyword); 
            return Ok(result);
        }

        // PUT: api/customerProfile/5
        /// <summary align="left">
        /// Modifies a record on the Database.
        /// </summary>
        /// <param name="id">ID of the record. Can be found using GET for all Coustomers</param>
        /// <param name="customerprofile"></param>
        /// <returns>The updated record</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="404">If no result found</response>
        [HttpPut("{id}")]    
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/customerProfile
        ///     {
        ///        "name": "Nirav",
        ///        "address": "Swanston street, Melbourne, VIC, 3000",
        ///        "phoneNumber": "0400400400",
        ///        "dateOfBirth": "26/09/1986"
        ///     }
        ///
        /// </remarks>
        /// <param name="customerprofile"></param>
        /// <returns>A newly created Customer</returns>
        /// <response code="200">Returns the newly created Customer</response>
        /// <response code="400">Bad Request</response> 
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
        /// <summary align="left">
        /// Deletes a record on the Database.
        /// </summary>
        /// <param name="id">ID of the record to be deleted. Can be found using GET for all Coustomers</param>
        /// <returns>The deleted record</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="404">If no result found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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