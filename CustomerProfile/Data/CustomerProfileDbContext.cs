using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CustomerProfile.Models;

namespace CustomerProfile.Data
{
    public class CustomerProfileDbContext : DbContext
    {
        public CustomerProfileDbContext (DbContextOptions<CustomerProfileDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerProfile.Models.customerprofile> customerprofile { get; set; }
    }
}
