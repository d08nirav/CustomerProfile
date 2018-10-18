using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProfile.Models
{
    public class customerprofile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerProfileID { get; set; }
        //[Key, Column(Order = 0)]
        public string Name { get; set; }
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        //[Key, Column(Order = 1)]
        public string PhoneNumber { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        //[Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }
    }
}

// GET: api/customerProfile/Nirav
/*[HttpGet("{name}")]
public IEnumerable<customerprofile> GetcustomerprofilebyName([FromRoute] string name)
{
    return _context.customerprofile.Where(s => s.Name == name);
}*/
