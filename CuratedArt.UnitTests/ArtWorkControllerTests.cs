using CuratedArt.Controllers;
using CuratedArt.Dtos;
using CuratedArt.Services;
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
            _artWorkServiceFake.GetArtWorks().Returns(new List<ArtWorkDto> { });

            var result = await _sut.Get();

            Assert.IsType<List<ArtWorkDto>>(result.Value);
        }
    }
}
