namespace MoviesApi.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAll();

        Task<Genre> GetById(byte id);

        Task<Genre> AddGenre(Genre genre);

        Genre Update(Genre genre);

        Genre Delete(Genre genre);

        Task<bool> IsValidGenre(byte id);
    }
}
