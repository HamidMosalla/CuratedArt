using CuratedArt.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CuratedArt.IntegrationTests.Setup
{
    public class IntegrationTestBase : IClassFixture<IntegrationTestFactory<Program, CuratedArtDbContext>>
    {
        public readonly IntegrationTestFactory<Program, CuratedArtDbContext> Factory;
        public CuratedArtDbContext CuratedArtDbContext => GetCuratedArtDbContext();

        public IntegrationTestBase(IntegrationTestFactory<Program, CuratedArtDbContext> factory)
        {
            Factory = factory;
        }

        public CuratedArtDbContext GetCuratedArtDbContext()
        {
            using var scope = Factory.Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<CuratedArtDbContext>();
        }

    }
}
