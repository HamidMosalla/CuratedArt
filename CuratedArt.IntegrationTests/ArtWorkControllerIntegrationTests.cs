using System.Net;
using System.Net.Http.Json;
using CuratedArt.Data;
using CuratedArt.Data.Models;
using CuratedArt.Dtos;
using CuratedArt.IntegrationTests.Setup;

namespace CuratedArt.IntegrationTests
{
    public class ArtWorkControllerIntegrationTests : IntegrationTestBase
    {
        public ArtWorkControllerIntegrationTests(IntegrationTestFactory<Program, CuratedArtDbContext> factory) : base(factory) { }

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
                new ArtWork{ Id = Guid.NewGuid(), DateReleased = DateTime.Now , Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Movie},
            };

            await CuratedArtDbContext.AddRangeAsync(artWorks);
            await CuratedArtDbContext.SaveChangesAsync();
        }

        public async Task<Guid> AddArtWorkAsync()
        {
            var artWork = new ArtWork { Id = Guid.NewGuid(), DateReleased = DateTime.Now, Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Movie };

            await CuratedArtDbContext.AddAsync(artWork);
            await CuratedArtDbContext.SaveChangesAsync();

            return artWork.Id;
        }

        public ArtWorkDto CreateArtWorkDto()
        {
            return new ArtWorkDto { Id = Guid.NewGuid(), DateReleased = DateTime.Now, Name = "Artwork1", Desc = "desc for art work 1", Type = ArtWorkType.Movie };
        }


        [Fact]
        public async Task GetAllArtWorks_WhenCalled_AlwaysReturnsAllArtWorks()
        {
            await AddArtWorksAsync();

            var client = Factory.CreateClient();

            // Arrange
            var artWorksReponse = await client.GetFromJsonAsync<IList<ArtWorkDto>>("/api/v1/artworks");

            // Assert
            Assert.NotNull(artWorksReponse);
            Assert.True(artWorksReponse.Count == 11);
            Assert.IsType<List<ArtWorkDto>>(artWorksReponse);
        }

        [Fact]
        public async Task GetArtWorksById_WhenCalled_ReturnsTheCorrectArtWork()
        {
            var artWorkId = await AddArtWorkAsync();

            var client = Factory.CreateClient();

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

            var client = Factory.CreateClient();

            // Arrange
            var artWorksReponse = await client.PostAsJsonAsync<ArtWorkDto>($"/api/v1/artworks", artWorkDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, artWorksReponse.StatusCode);
            Assert.NotNull(artWorksReponse);
            Assert.IsType<ArtWorkDto>(artWorksReponse);
        }

        [Fact]
        public async Task PatchArtWork_WhenCalled_CorrectlyPatchesTheArtWork()
        {
            var artWorkId = await AddArtWorkAsync();

            var client = Factory.CreateClient();

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

            var httpContent = new StringContent(body);

            // Arrange
            var artWorksReponse = await client.PatchAsync($"/api/v1/artworks/{artWorkId}", httpContent);

            var artWork = CuratedArtDbContext.ArtWorks.Find(artWorkId);

            // Assert
            Assert.Equal(HttpStatusCode.OK, artWorksReponse.StatusCode);
            Assert.True(artWork.DateReleased == default(DateTimeOffset));
            Assert.True(artWork.Name == "Mandy");
        }

        [Fact]
        public async Task DeleteArtWork_WhenCalled_ReturnsTheCorrectArtWork()
        {
            var artWorkDto = CreateArtWorkDto();

            var client = Factory.CreateClient();

            // Arrange
            var artWorksReponse = await client.DeleteAsync($"/api/v1/artworks/{artWorkDto.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, artWorksReponse.StatusCode);

            var nonExistingArtWork = CuratedArtDbContext.ArtWorks.SingleOrDefault(a => a.Id == artWorkDto.Id);

            Assert.Null(nonExistingArtWork);
        }
    }
}