using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerProfile.Models
{
    public class customerprofile
    {
        [Key]
        public int CustomerProfileID { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
