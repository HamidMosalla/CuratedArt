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
        public async Task PostArtWorks_WhenCalled_ReturnsTheCorrectArtWork()
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
    }
}