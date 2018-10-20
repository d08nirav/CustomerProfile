using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CustomerProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        /*// GET api/test
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Connection Testing: GET method working prefectly" };
        }*/

        /// <summary>  
        /// To test GET method.   
        /// </summary>
        /// <param name="id">The value that will be returned when testing is successful</param>

        // GET api/test/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "Connection Testing: GET method working prefectly, received value: " + id;
        }

        /// <summary>  
        /// To test POST method.   
        /// </summary>
        // POST api/test
        [HttpPost]
        public ActionResult<string> Post([FromBody] string value)
        {
            return "Connection Testing: POST method working prefectly, received value from body: " +value;
        }

        /// <summary>  
        /// To test PUT method.   
        /// </summary>
        /// <param name="id">The id that will be returned when testing is successful</param>
        /// <param name="value">The value in body that will be returned when testing is successful</param>
        /// <returns>A JSON list of all movies.</returns>
        // PUT api/test/5
        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] string value)
        {
            return "Connection Testing: PUT method working prefectly, received value: "+ id+" from body: " + value;
        }

        /// <summary>  
        /// To test DELETE method.   
        /// </summary>
        /// <param name="id">The id that will be returned when testing is successful</param>
        // DELETE api/test/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            return "Connection Testing: DELETE method working prefectly, received value: " + id;
        }
    }
}
