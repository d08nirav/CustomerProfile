using CustomerProfile;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace XUnitTestCustomerProfile
{
    public class customerProfileIntegrationTest
    {
        private readonly HttpClient _client;

        public customerProfileIntegrationTest()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var server = new TestServer(new WebHostBuilder().ConfigureAppConfiguration(config=> { config.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();  }).UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        public void GetcustomerprofileTest()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "api/customerProfile");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Nirav")]
        public void GetcustomerprofilebyNameTest(String Name)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"api/customerProfile/{Name}");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void DeletecustomerprofileTest(int id)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("DELETE"), $"api/customerProfile/{id}");

            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void EditTest(int id)
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("PUT"), $"api/customerProfile/{id}");
            request.Content = new StringContent("{\"name\":\"John Doe\",\"address\":\"Melb\",\"phoneNumber\":\"0400400400\",\"dateOfBirth\":\"01-01-2001\"}", Encoding.UTF8, "application/json");
            
            //Act
            var response = _client.SendAsync(request).Result;

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
