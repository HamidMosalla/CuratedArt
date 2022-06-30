namespace CuratedArt.Data.Models
{
    public class ArtWork
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Desc { get; set; }

        public DateTimeOffset DateReleased { get; set; }

        public ArtWorkType Type { get; set; }
    }

    public enum ArtWorkType
    {
        Painting,
        Music,
        Movie
    }
}
