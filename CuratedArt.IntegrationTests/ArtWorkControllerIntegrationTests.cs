using System.Net;
using CuratedArt.Data;
using CuratedArt.IntegrationTests.Setup;

namespace CuratedArt.IntegrationTests
{
    public class ArtWorkControllerIntegrationTests : IntegrationTestBase
    {
        public ArtWorkControllerIntegrationTests(IntegrationTestFactory<Program, CuratedArtDbContext> factory) : base(factory) { }

        [Fact]
        public async Task TestHomePage()
        {
            var context = CuratedArtDbContext;

            var client = Factory.CreateClient();

            // Arrange
            var defaultPage = await client.GetAsync("/");

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
        }
    }
}