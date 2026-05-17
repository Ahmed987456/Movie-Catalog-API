
namespace MoviesApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly AppDBContext _context;

        public MovieService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;   
        }

        public Movie DeleteMovie(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges(); 
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genreid=0)
        {
         return await _context.Movies
                .Where(s=>s.GenreId==genreid||genreid==0)
                .Include(s => s.Genre)
                .ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(s => s.Id ==id);
        }

        public Movie UpdateMovie(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
