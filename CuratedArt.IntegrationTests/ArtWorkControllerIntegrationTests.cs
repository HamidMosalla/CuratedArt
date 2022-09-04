using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using CuratedArt.Data;
using CuratedArt.Data.Models;
using CuratedArt.Dtos;
using CuratedArt.IntegrationTests.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CuratedArt.IntegrationTests
{
    public class ArtWorkControllerIntegrationTests : IClassFixture<IntegrationTestFactory<Program, CuratedArtDbContext>>
    {
        private readonly CuratedArtDbContext _curatedArtDbContext;
        private readonly IntegrationTestFactory<Program, CuratedArtDbContext> _integrationTestFactory;

        public ArtWorkControllerIntegrationTests(IntegrationTestFactory<Program, CuratedArtDbContext> factory)
        {
            _integrationTestFactory = factory;
            var scope = _integrationTestFactory.Services.CreateScope();
            _curatedArtDbContext = scope.ServiceProvider.GetRequiredService<CuratedArtDbContext>();
        }

        public async Task AddArtWorksAsync()
        {
            var artWorks = new List<ArtWork>
            {
                new ArtWork{ Id = Guid.NewGuid(), DateReleased = DateTime.Now , Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Movie},
                new ArtWork{ Id = Guid.NewGuid(), DateReleased = DateTime.Now , Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Music},
                new ArtWork{ Id = Guid.NewGuid(), DateReleased = DateTime.Now , Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Painting},
                new ArtWork{ Id = Guid.NewGuid(), DateReleased = DateTime.Now , Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Painting},
                new ArtWork{ Id = Guid.NewGuid(), DateReleased = DateTime.Now , Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Movie},
                new ArtWork{ Id = Guid.NewGuid(), DateReleased = DateTime.Now , Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Music},
                new ArtWork{ Id = Guid.NewGuid(), DateReleased = DateTime.Now , Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Painting},
                new ArtWork{ Id = Guid.NewGuid(), DateReleased = DateTime.Now , Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Music},
                new ArtWork{ Id = Guid.NewGuid(), DateReleased = DateTime.Now , Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Painting},
                new ArtWork{ Id = Guid.NewGuid(), DateReleased = DateTime.Now , Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Music},
            };

            await _curatedArtDbContext.AddRangeAsync(artWorks);
            await _curatedArtDbContext.SaveChangesAsync();
        }

        public async Task<Guid> AddArtWorkAsync()
        {
            var artWork = new ArtWork { Id = Guid.NewGuid(), DateReleased = DateTime.Now, Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Movie };

            await _curatedArtDbContext.AddAsync(artWork);
            await _curatedArtDbContext.SaveChangesAsync();

            return artWork.Id;
        }

        public ArtWorkDto CreateArtWorkDto()
        {
            return new ArtWorkDto { Id = Guid.NewGuid(), DateReleased = DateTime.Now, Name = "Artwork1Joe", Desc = "desc for art work 1", Type = ArtWorkType.Movie };
        }


        [Fact]
        public async Task GetAllArtWorks_WhenCalled_AlwaysReturnsAllArtWorks()
        {
            await AddArtWorksAsync();

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
            var artWorkId = await AddArtWorkAsync();

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
            var artWorkDto = CreateArtWorkDto();

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
            var inTheDb = _curatedArtDbContext.ArtWorks.SingleOrDefault(a => a.Id == artWorkDto.Id);
            Assert.NotNull(inTheDb);
            Assert.True(inTheDb.Name == "Artwork1Joe");
        }

        [Fact]
        public async Task PatchArtWork_WhenCalled_CorrectlyPatchesTheArtWork()
        {
            var artWorkId = await AddArtWorkAsync();

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

            _curatedArtDbContext.ChangeTracker.Clear();

            var artWork = await _curatedArtDbContext.ArtWorks.SingleOrDefaultAsync(a=> a.Id  == artWorkId);

            // Assert
            Assert.Equal(HttpStatusCode.OK, artWorksReponse.StatusCode);
            Assert.True(artWork.DateReleased == default(DateTimeOffset));
            Assert.True(artWork.Name == "Mandy");
        }

        [Fact]
        public async Task DeleteArtWork_WhenCalled_ReturnsTheCorrectArtWork()
        {
            var artWorkDto = CreateArtWorkDto();

            var client = _integrationTestFactory.CreateClient();

            // Arrange
            var artWorksReponse = await client.DeleteAsync($"/api/v1/artworks/{artWorkDto.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, artWorksReponse.StatusCode);

            var nonExistingArtWork = _curatedArtDbContext.ArtWorks.SingleOrDefault(a => a.Id == artWorkDto.Id);

            Assert.Null(nonExistingArtWork);
        }
    }
}