
namespace MoviesApi.Services
{
    public class GenreService : IGenreService
    {
        private readonly AppDBContext _context;

        public GenreService(AppDBContext context)
        {
            _context = context;
        } 

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetById(byte id)
        {
            return await _context.Genres.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Genre> AddGenre(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre Delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();
            return genre;
        }

        public Task<bool> IsValidGenre(byte id)
        {
          return _context.Genres.AnyAsync(s => s.Id == id);
        }
    }
}
