namespace CuratedArt.Dtos;

using CuratedArt.Data.Models;

public class ArtWorkDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Desc { get; set; }

    public DateTimeOffset DateReleased { get; set; }

    public ArtWorkType Type { get; set; }
}