using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;





namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            var genres = await _genreService.GetAll();
            return Ok(genres);
        }

        [HttpPost]

        public async Task<IActionResult> CreateAsync(CreateGenreDto dto) {
            var genre = new Genre {  Name= dto.Name  };
           await _genreService.AddGenre(genre);
           return Ok(genre);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] CreateGenreDto dto) {
            var genre = await _genreService.GetById(id);
            if (genre == null)
                return NotFound($"No Genre was Found With Id {id}");

            genre.Name = dto.Name;

            _genreService.Update(genre);

            return Ok(genre);

        }

        
        [HttpDelete("{id}")]

        public async Task<IActionResult> DelteAsync(byte id)
        {
            var genre = await _genreService.GetById(id);
            if (genre == null)
                return NotFound($"No Genre was Found With Id {id}");

            _genreService.Delete(genre);

            return Ok(genre);

        }
    }
}
