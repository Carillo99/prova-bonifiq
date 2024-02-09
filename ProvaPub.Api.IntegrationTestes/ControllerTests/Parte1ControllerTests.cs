using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using ProvaPub.Api.IntegrationTests.Base;
using ProvaPub.API.Services;
using ProvaPub.Domain.Interfaces.IServices;
using System.Net;

namespace ProvaPub.Api.IntegrationTests.ControllerTests
{
    public class Parte1ControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly CustomWebApplicationFactory<Program> _factory;

        public Parte1ControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetRandom()
        {
            // Arrange
            var client = _factory.GetAnonymousClient();

            // Act
            var response = await client.GetAsync("/parte1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
