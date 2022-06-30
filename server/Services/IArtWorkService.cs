namespace CuratedArt.Services;

using CuratedArt.Dtos;

public interface IArtWorkService
{
    List<ArtWorkDto> GetArtWorks();
}