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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //builder.Entity<customerprofile>().HasKey(x => new { x.Name, x.PhoneNumber });
        }

        public DbSet<CustomerProfile.Models.customerprofile> customerprofile { get; set; }
    }
}
