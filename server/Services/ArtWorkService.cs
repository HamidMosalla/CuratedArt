using CuratedArt.Dtos;

namespace CuratedArt.Services;

using CuratedArt.Data;

public class ArtWorkService : IArtWorkService
{
    private CuratedArtDbContext _curatedArtDbContext;

    public ArtWorkService(CuratedArtDbContext curatedArtDbContext)
    {
        _curatedArtDbContext = curatedArtDbContext;
    }

    public List<ArtWorkDto> GetArtWorks()
    {
        var artWorks = _curatedArtDbContext.ArtWorks.Select(a => new ArtWorkDto
        { Id = a.Id, Name = a.Name, Desc = a.Desc, DateReleased = a.DateReleased, Type = a.Type }).ToList();

        return artWorks;
    }
}