using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProvaPub.Api.IntegrationTests.Base;
using ProvaPub.API.Services;
using ProvaPub.Domain.DTO;
using ProvaPub.Domain.Interfaces.IServices;
using System.Net;
using System.Text;

namespace ProvaPub.Api.IntegrationTests.ControllerTests
{
    public class Parte4ControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly CustomWebApplicationFactory<Program> _factory;

        public Parte4ControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCanPurchase()
        {
            // Arrange
            var client = _factory.GetAnonymousClient();

            // Act
            var response = await client.GetAsync("/parte4/CanPurchase?customerId=1&purchaseValue=4");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
