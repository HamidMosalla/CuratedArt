using CuratedArt.Data.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace CuratedArt.Services;

using Dtos;

public interface IArtWorkService
{
    Task<List<ArtWorkDto>> GetArtWorks();
    Task<ArtWorkDto?> GetArtWork(Guid id);
    Task DeleteArtWork(Guid id);
    Task<ArtWorkDto> CreateArtWork(ArtWorkDto artWorkDto);
    Task PatchArtWork(JsonPatchDocument<ArtWorkDto> patchDocument, ArtWorkDto artWork);
    Task<ArtWorkDto[]> CreateArtWorks(ArtWorkDto[] artWorkDtos);
}