    namespace MoviesApi.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAll(byte genreid =0);

        Task<Movie> GetById(int id);

        Task<Movie> AddMovie(Movie movie);

        Movie UpdateMovie(Movie movie);

        Movie DeleteMovie(Movie movie);
    }
}
