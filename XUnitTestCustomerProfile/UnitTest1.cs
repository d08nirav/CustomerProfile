using CustomerProfile.Controllers;
using CustomerProfile.Data;
using CustomerProfile.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestCustomerProfile
{
    public class UnitTest1
    {
        customerProfileController _customerProfileController;
        CustomerProfileDbContext _context;

        public UnitTest1()
        {
            var dbOption = new DbContextOptionsBuilder<CustomerProfileDbContext>().UseSqlServer("Server = d08nirav.database.windows.net; Initial Catalog = customer_profile; User ID = d08nirav; Password = Dcba1234!; MultipleActiveResultSets = False; Encrypt = True; ").Options;
            _context = new CustomerProfileDbContext(dbOption);
            _customerProfileController = new customerProfileController(_context);
        }

        [Fact]
        public void Getcustomerprofile_ReturnsCustomerProfile()
        {
            // Act
            var okResult = _customerProfileController.Getcustomerprofile();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<customerprofile>>(okResult);
        }
    }
}
