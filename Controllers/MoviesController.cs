using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private new List<string> _allowedExtenstion = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;
        private readonly IMovieService _movieServce;
        private readonly IGenreService _genreServce;
        private readonly IMapper _mapper;

        public MoviesController(IGenreService genreServce, IMovieService movieServce, IMapper mapper)
        {
            _genreServce = genreServce;
            _movieServce = movieServce;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _movieServce.GetAll();
            var movi = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(movi);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _movieServce.GetById(id);
            if (movie == null)
                return BadRequest($"Not Id Found With Id {id}");
            var dto = _mapper.Map<MovieDetailsDto>(movie);
            return Ok(dto);
        }

        [HttpGet("GetByGernreId")]
        public async Task<IActionResult> GetByGernreIdAsync(byte GenreId)
        {
            var movies = await _movieServce.GetAll(GenreId);
            var movie = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync( [FromForm] CreadtMovieDto dto) 
        {
            if (!_allowedExtenstion.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png or .jpg images are allwed!");

            if(dto.Poster.Length>_maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");

            var isValiadGenre = await _genreServce.IsValidGenre(dto.GenreId);
             if (!isValiadGenre)
                return BadRequest("Invalide Genre Id");

            using var DataStream= new MemoryStream();
            await dto.Poster.CopyToAsync(DataStream);
            var movie = _mapper.Map<Movie>(dto);
            movie.Poster=DataStream.ToArray();  
            _movieServce.AddMovie(movie);
            return Ok(movie);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateMovieDto dto)
        {

            var movie = await _movieServce.GetById(id);

            if (movie == null)
                return BadRequest($"Not Id Found With Id {id}");

            var isValiadGenre = await _genreServce.IsValidGenre(dto.GenreId);
            if (!isValiadGenre)
                return BadRequest("Invalide Genre Id");

            if (dto.Poster != null)
            {
                if (!_allowedExtenstion.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png or .jpg images are allwed!");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1MB!");
                using var DataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(DataStream);
                movie.Poster = DataStream.ToArray();
            }

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Storeline = dto.Storeline;
            movie.GenreId = dto.GenreId;
            movie.Rate = dto.Rate;
           
            _movieServce.UpdateMovie(movie);
            return Ok(movie);

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync(int id) 
        {
            var movie = await _movieServce.GetById(id);
            if (movie == null)
                return BadRequest($"Not Id Found With Id {id}");
            _movieServce.DeleteMovie(movie);
            return Ok(movie);
        }

        
    }
}
