using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using CuratedArt.Data;
using CuratedArt.Dtos;
using CuratedArt.IntegrationTests.Creators;
using CuratedArt.IntegrationTests.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CuratedArt.IntegrationTests
{
    public class ArtWorkControllerIntegrationTests : IntegrationTestBase
    {
        private readonly IntegrationTestFactory<Program, CuratedArtDbContext> _integrationTestFactory;
        private readonly ArtworkCreator _artworkCreator;

        public ArtWorkControllerIntegrationTests(IntegrationTestFactory<Program, CuratedArtDbContext> factory) : base(factory)
        {
            _integrationTestFactory = factory;
            var scope = factory.Services.CreateScope();
            _artworkCreator = scope.ServiceProvider.GetRequiredService<ArtworkCreator>();
        }

        [Fact]
        public async Task GetAllArtWorks_WhenCalled_AlwaysReturnsAllArtWorks()
        {
            await _artworkCreator.AddArtWorksAsync();

            var client = _integrationTestFactory.CreateClient();

            // Arrange
            var artWorksReponse = await client.GetFromJsonAsync<IList<ArtWorkDto>>("/api/v1/artworks");

            // Assert
            Assert.NotNull(artWorksReponse);
            Assert.True(artWorksReponse.Count == 20);
            Assert.IsType<List<ArtWorkDto>>(artWorksReponse);
        }

        [Fact]
        public async Task GetArtWorksById_WhenCalled_ReturnsTheCorrectArtWork()
        {
            var artWorkId = await _artworkCreator.AddArtWorkAsync();

            var client = _integrationTestFactory.CreateClient();

            // Arrange
            var artWorksReponse = await client.GetFromJsonAsync<ArtWorkDto>($"/api/v1/artworks/{artWorkId}");

            // Assert
            Assert.NotNull(artWorksReponse);
            Assert.IsType<ArtWorkDto>(artWorksReponse);
            Assert.True(artWorkId == artWorksReponse.Id);
        }

        [Fact]
        public async Task PostArtWork_WhenCalled_ReturnsTheCorrectArtWork()
        {
            var artWorkDto = ArtworkCreator.CreateArtWorkDto();

            var client = _integrationTestFactory.CreateClient();

            // Arrange
            var artWorksReponse = await client.PostAsJsonAsync<ArtWorkDto>($"/api/v1/artworks", artWorkDto);

            // Assert
            Assert.Equal(HttpStatusCode.Created, artWorksReponse.StatusCode);
            Assert.NotNull(artWorksReponse);
            var artWorkReponseArtWorkDto = await artWorksReponse.Content.ReadFromJsonAsync<ArtWorkDto>();
            Assert.IsType<ArtWorkDto>(artWorkReponseArtWorkDto);
            Assert.True(artWorkReponseArtWorkDto.Name == artWorkDto.Name);
            // ATTENTION: it did save it in the service
            var inTheDb = DbContext.ArtWorks.SingleOrDefault(a => a.Id == artWorkDto.Id);
            Assert.NotNull(inTheDb);
            Assert.True(inTheDb.Name == "Artwork1Joe");
        }

        [Fact]
        public async Task PatchArtWork_WhenCalled_CorrectlyPatchesTheArtWork()
        {
            var artWorkId = await _artworkCreator.AddArtWorkAsync();

            var client = _integrationTestFactory.CreateClient();

            var body = @"[
                        " + "\n" +
                        @"  {
                        " + "\n" +
                        @"    ""op"": ""replace"",
                        " + "\n" +
                        @"    ""path"": ""/name"",
                        " + "\n" +
                        @"    ""value"": ""Mandy""
                        " + "\n" +
                        @"  },
                        " + "\n" +
                        @"  {
                        " + "\n" +
                        @"    ""op"": ""remove"",
                        " + "\n" +
                        @"    ""path"": ""/dateReleased"",
                        " + "\n" +
                        @"    ""value"": null
                        " + "\n" +
                        @"  }
                        " + "\n" +
                        @"]";

            var httpContent = new StringContent(body, Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Arrange
            var artWorksReponse = await client.PatchAsync($"/api/v1/artworks/{artWorkId}", httpContent);

            // Note to self: There was a bug that causing the following singleordefault not return the fresh value
            // We clear the cache here, and it fixes it, but it's best for the creator classes be on their own, not in this class
            // DbContext.ChangeTracker.Clear();

            var artWork = await DbContext.ArtWorks.SingleOrDefaultAsync(a => a.Id == artWorkId);

            // Assert
            Assert.Equal(HttpStatusCode.OK, artWorksReponse.StatusCode);
            Assert.True(artWork.DateReleased == default(DateTimeOffset));
            Assert.True(artWork.Name == "Mandy");
        }

        [Fact]
        public async Task DeleteArtWork_WhenCalled_ReturnsTheCorrectArtWork()
        {
            var artWorkDto = ArtworkCreator.CreateArtWorkDto();

            var client = _integrationTestFactory.CreateClient();

            // Arrange
            var artWorksReponse = await client.DeleteAsync($"/api/v1/artworks/{artWorkDto.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, artWorksReponse.StatusCode);

            var nonExistingArtWork = DbContext.ArtWorks.SingleOrDefault(a => a.Id == artWorkDto.Id);

            Assert.Null(nonExistingArtWork);
        }
    }
}