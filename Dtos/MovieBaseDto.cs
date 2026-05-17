using Microsoft.AspNetCore.Mvc;

namespace MoviesApi.Dtos
{
    public class MovieBaseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        [MaxLength(2500)]
        public string Storeline { get; set; }

        public byte GenreId { get; set; }
    }
}
