namespace CuratedArt.Services;

using Dtos;

public interface IArtWorkService
{
    Task<List<ArtWorkDto>> GetArtWorks();
    Task<ArtWorkDto> GetArtWork(Guid id);
}