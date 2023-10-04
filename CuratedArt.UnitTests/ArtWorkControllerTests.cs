using CuratedArt.Controllers;
using CuratedArt.Data.Models;
using CuratedArt.Dtos;
using CuratedArt.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace CuratedArt.UnitTests
{
    public class ArtWorkControllerTests
    {
        private IArtWorkService _artWorkServiceFake = Substitute.For<IArtWorkService>();
        private ArtWorkController _sut;

        public ArtWorkControllerTests()
        {
            _sut = new ArtWorkController(_artWorkServiceFake);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsTheCorrectType()
        {
            _artWorkServiceFake.GetArtWorks().Returns(new List<ArtWorkDto>
            {
                new ArtWorkDto
                {
                    Id = Guid.NewGuid(),
                     Name = "Painting #2",
                     Type = ArtWorkType.Painting,
                     DateReleased = DateTimeOffset.Now,
                      Desc = "The second epic painting"
                }
            });

            var result = await _sut.Get();

            var artWorks = (result.Result as OkObjectResult)?.Value as List<ArtWorkDto>;

            Assert.NotEmpty(artWorks);
        }
    }
}
