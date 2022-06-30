using CuratedArt.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CuratedArt.Controllers
{
    using CuratedArt.Dtos;

    [Route("api/[controller]")]
    [ApiController]
    public class ArtWorkController : ControllerBase
    {
        private IArtWorkService _artWorkService;

        // GET: api/<ArtWorkController>
        public ArtWorkController(IArtWorkService artWorkService)
        {
            _artWorkService = artWorkService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var artWorks = _artWorkService.GetArtWorks();

            return Ok(artWorks);
        }

        // GET api/<ArtWorkController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ArtWorkController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ArtWorkController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArtWorkController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
