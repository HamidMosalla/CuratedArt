using System.Net;
using CuratedArt.Data;
using CuratedArt.IntegrationTests.Setup;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CuratedArt.IntegrationTests
{
    public class Tests : IClassFixture<IntegrationTestFactory<Program, CuratedArtDbContext>>
    {
        private readonly IntegrationTestFactory<Program, CuratedArtDbContext> _factory;

        public Tests(IntegrationTestFactory<Program, CuratedArtDbContext> factory) => _factory = factory;

        [Fact]
        public async Task TestHomePage()
        {
            var client = _factory.CreateClient();

            // Arrange
            var defaultPage = await client.GetAsync("/");

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
        }
    }
}