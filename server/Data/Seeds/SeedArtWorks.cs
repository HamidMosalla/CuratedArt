using CuratedArt.Data.Models;

namespace CuratedArt.Data.Seeds
{
    public class SeedArtWorks
    {
        private CuratedArtDbContext _artDbContext;

        public SeedArtWorks(CuratedArtDbContext artDbContext)
        {
            _artDbContext = artDbContext;
        }

        public Task Seed()
        {
            var artWorks = new List<ArtWork>
            {
                new ArtWork {  Name = "artWork1", Desc = "desc 1", Type = ArtWorkType.Movie, DateReleased = DateTimeOffset.MaxValue},
                new ArtWork {  Name = "artWork2", Desc = "desc 2", Type = ArtWorkType.Movie, DateReleased = DateTimeOffset.MaxValue},
                new ArtWork {  Name = "artWork3", Desc = "desc 3", Type = ArtWorkType.Movie, DateReleased = DateTimeOffset.MaxValue},
                new ArtWork {  Name = "artWork4", Desc = "desc 4", Type = ArtWorkType.Movie, DateReleased = DateTimeOffset.MaxValue},
                new ArtWork {  Name = "artWork5", Desc = "desc 5", Type = ArtWorkType.Movie, DateReleased = DateTimeOffset.MaxValue},
                new ArtWork {  Name = "artWork6", Desc = "desc 6", Type = ArtWorkType.Movie, DateReleased = DateTimeOffset.MaxValue},
                new ArtWork {  Name = "artWork7", Desc = "desc 7", Type = ArtWorkType.Movie, DateReleased = DateTimeOffset.MaxValue},
                new ArtWork {  Name = "artWork8", Desc = "desc 8", Type = ArtWorkType.Movie, DateReleased = DateTimeOffset.MaxValue},
                new ArtWork {  Name = "artWork9", Desc = "desc 9", Type = ArtWorkType.Movie, DateReleased = DateTimeOffset.MaxValue},
                new ArtWork {  Name = "artWork10", Desc = "desc 10", Type = ArtWorkType.Movie, DateReleased = DateTimeOffset.MaxValue},
            };

            _artDbContext.ArtWorks.AddRange(artWorks);
            return _artDbContext.SaveChangesAsync();
        }
    }
}
