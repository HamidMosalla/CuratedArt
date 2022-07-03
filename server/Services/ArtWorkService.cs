using CuratedArt.Data.Models;
using CuratedArt.Dtos;
using Microsoft.AspNetCore.JsonPatch;
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

    public async Task<ArtWorkDto?> GetArtWork(Guid id)
    {
        var artWork = await _curatedArtDbContext.ArtWorks.FindAsync(id);

        if (artWork == null) return null;

        return new ArtWorkDto
        {
            Id = artWork.Id,
            Name = artWork.Name,
            Desc = artWork.Desc,
            DateReleased = artWork.DateReleased,
            Type = artWork.Type
        };
    }

    public async Task DeleteArtWork(Guid id)
    {
        var artWork = await _curatedArtDbContext.ArtWorks.FirstOrDefaultAsync(a => a.Id == id);

        if (artWork != null)
        {
            _curatedArtDbContext.ArtWorks.Remove(artWork);
            await _curatedArtDbContext.SaveChangesAsync();
        }
    }

    public async Task<ArtWorkDto> CreateArtWork(ArtWorkDto artWorkDto)
    {
        var artWork = new ArtWork
        {
            Id = artWorkDto.Id == Guid.Empty ? Guid.NewGuid() : artWorkDto.Id,
            Type = artWorkDto.Type,
            Name = artWorkDto.Name,
            DateReleased = artWorkDto.DateReleased,
            Desc = artWorkDto.Desc,
        };

        await _curatedArtDbContext.ArtWorks.AddAsync(artWork);
        await _curatedArtDbContext.SaveChangesAsync();

        return artWorkDto;
    }

    public async Task<ArtWorkDto[]> CreateArtWorks(ArtWorkDto[] artWorkDtos)
    {
        var artWorks = artWorkDtos.Select(a => new ArtWork
        {
            Id = a.Id == Guid.Empty ? Guid.NewGuid() : a.Id,
            Type = a.Type,
            Name = a.Name,
            DateReleased = a.DateReleased,
            Desc = a.Desc,
        });

        await _curatedArtDbContext.AddRangeAsync(artWorks);
        await _curatedArtDbContext.SaveChangesAsync();

        return artWorkDtos;
    }

    public async Task PatchArtWork(JsonPatchDocument<ArtWorkDto> patchDocument, ArtWorkDto artWorkDto)
    {
        patchDocument.ApplyTo(artWorkDto);

        // Meh, didn't want to install mapper
        var artWork = await _curatedArtDbContext.ArtWorks.FindAsync(artWorkDto.Id);

        artWork.Id = artWorkDto.Id;
        artWork.Name = artWorkDto.Name;
        artWork.Desc = artWorkDto.Desc;
        artWork.DateReleased = artWorkDto.DateReleased;
        artWork.Type = artWorkDto.Type;

        await _curatedArtDbContext.SaveChangesAsync();
    }
}
