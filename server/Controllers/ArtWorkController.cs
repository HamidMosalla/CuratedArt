using CuratedArt.Data;

namespace CuratedArt.Controllers
{
    using Services;
    using Microsoft.AspNetCore.Mvc;
    using Dtos;
    using Microsoft.AspNetCore.JsonPatch;

    [Route("api/v{version:apiVersion}/artworks")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ArtWorkController : ControllerBase
    {
        private IArtWorkService _artWorkService;
        private CuratedArtDbContext _curatedArtDbContext;

        // GET: api/<ArtWorkController>
        public ArtWorkController(IArtWorkService artWorkService, CuratedArtDbContext curatedArtDbContext)
        {
            _artWorkService = artWorkService;
            _curatedArtDbContext = curatedArtDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<ArtWorkDto>>> Get()
        {
            var artWorks = await _artWorkService.GetArtWorks();

            return Ok(artWorks);
        }

        [HttpGet("{id}", Name = "GetArtWork")]
        public async Task<ActionResult<ArtWorkDto>> Get(Guid id)
        {
            var artWork = await _artWorkService.GetArtWork(id);

            return Ok(artWork);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ArtWorkDto artWorkDto)
        {
            var createdArtWorkDto = await _artWorkService.CreateArtWork(artWorkDto);

            return CreatedAtRoute("GetArtWork", new { id = artWorkDto.Id }, createdArtWorkDto);
        }

        [HttpPost("bulk")]
        public async Task<ActionResult> BulkCreate([FromBody] ArtWorkDto[] artWorkDtos)
        {
            var createdArtWorkDtos = await _artWorkService.CreateArtWorks(artWorkDtos);

            return Ok(createdArtWorkDtos);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<ArtWorkDto> patchDocument)
        {
            var artWork = await _artWorkService.GetArtWork(id);

            if (artWork == null)
            {
                return NotFound();
            }

            await _artWorkService.PatchArtWork(patchDocument, artWork);

            return Ok(artWork);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _artWorkService.DeleteArtWork(id);

            return NoContent();
        }

        [HttpDelete("bulk-delete")]
        public async Task<ActionResult> BulkDelete([FromBody] Guid[] ids)
        {
            await _artWorkService.DeleteArtWorks(ids);

            return NoContent();
        }
    }
}
