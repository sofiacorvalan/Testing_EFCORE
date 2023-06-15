using Xunit;
using Testing.Models;
using Microsoft.EntityFrameworkCore;

namespace Testing.Tests
{
    public class MovieTests
    {
        private readonly MovieDbContext _context;

        public MovieTests()
        {
            _context = CreateInMemoryDbContext();
        }

        private MovieDbContext CreateInMemoryDbContext()
        {
            var services = new ServiceCollection()
                .AddDbContext<MovieDbContext>(options =>
                    options.UseInMemoryDatabase("moviesdb"))
                .BuildServiceProvider();

            return services.GetRequiredService<MovieDbContext>();
        }


        [Fact]
        public void TestAddMovie()
        {
            var newMovie = new Movie
            {
                Movie_name = "Shutter Island",
                Movie_genre = "Thriller",
                Movie_duration = 138,
                Movie_budget = 65000000
            };

            _context.Movies.Add(newMovie);
            _context.SaveChanges();

            var movie = _context.Movies.Find(newMovie.Id);
            Assert.NotNull(movie);
            Assert.Equal("Shutter Island", movie.Movie_name);
            Assert.Equal("Thriller", movie.Movie_genre);
            Assert.Equal(138, movie.Movie_duration);
            Assert.Equal(65000000, movie.Movie_budget);
        }

        [Fact]
        public void TestDeleteMovie()
        {
            var newMovie = new Movie
            {
                Movie_name = "Shutter Island",
                Movie_genre = "Thriller",
                Movie_duration = 138,
                Movie_budget = 65000000
            };

            _context.Movies.Add(newMovie);
            _context.SaveChanges();

            _context.Movies.Remove(newMovie);
            _context.SaveChanges();

            var movie = _context.Movies.Find(newMovie.Id);
            Assert.Null(movie);
        }

        [Fact]
        public void TestUpdateMovie()
        {
            var newMovie = new Movie
            {
                Movie_name = "Shutter Island",
                Movie_genre = "Thriller",
                Movie_duration = 138,
                Movie_budget = 65000000
            };

            _context.Movies.Add(newMovie);
            _context.SaveChanges();

            var movie = _context.Movies.Find(newMovie.Id);
            movie.Movie_name = "Inception";
            movie.Movie_budget = 292000000;
            _context.SaveChanges();

            var updatedMovie = _context.Movies.Find(newMovie.Id);
            Assert.NotNull(updatedMovie);
            Assert.Equal("Inception", updatedMovie.Movie_name);
            Assert.Equal(292000000, updatedMovie.Movie_budget);
        }
    }
}