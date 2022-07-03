using CuratedArt.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuratedArt.Controllers
{
    using CuratedArt.Dtos;

    [Route("api/v{version:apiVersion}/artworks")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ArtWorkController : ControllerBase
    {
        private IArtWorkService _artWorkService;

        // GET: api/<ArtWorkController>
        public ArtWorkController(IArtWorkService artWorkService)
        {
            _artWorkService = artWorkService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ArtWorkDto>>> Get()
        {
            var artWorks = await _artWorkService.GetArtWorks();

            return Ok(artWorks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtWorkDto>> Get(Guid id)
        {
            var artWork = await _artWorkService.GetArtWork(id);

            return Ok(artWork);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
