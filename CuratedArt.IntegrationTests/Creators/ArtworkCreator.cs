using CuratedArt.Data;
using CuratedArt.Data.Models;
using CuratedArt.Dtos;

namespace CuratedArt.IntegrationTests.Creators
{
    public class ArtworkCreator
    {
        private readonly CuratedArtDbContext _curatedArtDbContext;

        public ArtworkCreator(CuratedArtDbContext curatedArtDbContext)
        {
            _curatedArtDbContext = curatedArtDbContext;
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

        public static ArtWorkDto CreateArtWorkDto()
        {
            return new ArtWorkDto { Id = Guid.NewGuid(), DateReleased = DateTime.Now, Name = "Artwork1Joe", Desc = "desc for art work 1", Type = ArtWorkType.Movie };
        }
    }
}
