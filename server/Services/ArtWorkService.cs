using CuratedArt.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CuratedArt.Services;

using CuratedArt.Data;

public class ArtWorkService : IArtWorkService
{
    private CuratedArtDbContext _curatedArtDbContext;

    public ArtWorkService(CuratedArtDbContext curatedArtDbContext)
    {
        _curatedArtDbContext = curatedArtDbContext;
    }

    public Task<List<ArtWorkDto>> GetArtWorks()
    {
        var artWorks = _curatedArtDbContext.ArtWorks.Select(a => new ArtWorkDto
        { Id = a.Id, Name = a.Name, Desc = a.Desc, DateReleased = a.DateReleased, Type = a.Type }).ToListAsync();

        return artWorks;
    }

    public async Task<ArtWorkDto>  GetArtWork(Guid id)
    {
        var artWork =  await _curatedArtDbContext.ArtWorks.FindAsync(id);

        return new ArtWorkDto
        {
            Id = artWork.Id, Name = artWork.Name, Desc = artWork.Desc, DateReleased = artWork.DateReleased,
            Type = artWork.Type
        };
    }
}
