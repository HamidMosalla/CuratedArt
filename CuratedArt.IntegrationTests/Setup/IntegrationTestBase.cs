using CuratedArt.Data;

namespace CuratedArt.IntegrationTests.Setup
{
    public class IntegrationTestBase : IClassFixture<IntegrationTestFactory<Program, CuratedArtDbContext>>
    {
        public readonly IntegrationTestFactory<Program, CuratedArtDbContext> Factory;

        public IntegrationTestBase(IntegrationTestFactory<Program, CuratedArtDbContext> factory)
        {
            Factory = factory;
        }
    }
}
