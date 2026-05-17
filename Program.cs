using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using MoviesApi.Data;

namespace MoviesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors();
            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDBContext>(options=>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection")) );
            builder.Services.AddTransient<IGenreService,GenreService>();
            builder.Services.AddTransient<IMovieService, MovieService>();
            builder.Services.AddAutoMapper(typeof(Program));
            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
