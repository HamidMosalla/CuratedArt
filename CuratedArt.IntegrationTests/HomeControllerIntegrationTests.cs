using System.Net;
using CuratedArt.Data;
using CuratedArt.IntegrationTests.Setup;

namespace CuratedArt.IntegrationTests
{
    public class HomeControllerIntegrationTests : IntegrationTestBase
    {
        public HomeControllerIntegrationTests(IntegrationTestFactory<Program, CuratedArtDbContext> factory) : base(factory) { }

        [Fact]
        public async Task CalledToHomePage_Always_ReceiveOKStatusCode()
        {
            var client = Factory.CreateClient();

            // Arrange
            var defaultPage = await client.GetAsync("/");

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
        }
    }
}