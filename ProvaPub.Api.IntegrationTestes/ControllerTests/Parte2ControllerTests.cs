using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProvaPub.Api.IntegrationTests.Base;
using ProvaPub.API.Services;
using ProvaPub.Domain.DTO;
using ProvaPub.Domain.DTO.Report;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Domain.Models;
using System.Net;
using System.Reflection.Metadata;
using System.Text;

namespace ProvaPub.Api.IntegrationTests.ControllerTests
{
    public class Parte2ControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly CustomWebApplicationFactory<Program> _factory;

        public Parte2ControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCustomers()
        {
            int rows = 10;

            // Arrange
            var client = _factory.GetAnonymousClient();

            var filter = new FilterList() { Page = 0, Rows = rows };
            var jsonContent = JsonConvert.SerializeObject(filter);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/parte2/customers", contentString);
            var content = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<CustomerDTOList>(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(rows, customers.Customers.Count);
        }

        [Fact]
        public async Task GetProduct()
        {
            int rows = 10;

            // Arrange
            var client = _factory.GetAnonymousClient();

            var filter = new FilterList() { Page = 0, Rows = rows };
            var jsonContent = JsonConvert.SerializeObject(filter);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/parte2/products", contentString);
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<ProductDTOList>(content);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(rows, products.Products.Count);
        }
    }
}
