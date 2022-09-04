using CuratedArt.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CuratedArt.IntegrationTests.Setup
{
    public class IntegrationTestBase : IClassFixture<IntegrationTestFactory<Program, CuratedArtDbContext>>
    {
        public readonly IntegrationTestFactory<Program, CuratedArtDbContext> Factory;
        public readonly CuratedArtDbContext DbContext;

        public IntegrationTestBase(IntegrationTestFactory<Program, CuratedArtDbContext> factory)
        {
            Factory = factory;
            var scope = factory.Services.CreateScope();
            DbContext = scope.ServiceProvider.GetRequiredService<CuratedArtDbContext>();
        }
    }
}
