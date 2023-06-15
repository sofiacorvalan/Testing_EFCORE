using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testing.Models;

namespace Testing.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddDbContext<MovieDbContext>(options =>
                 options.UseMySql("server=localhost;database=moviesbd;user=root;password=SOFIApaola1997;ConnectionTimeout=30;",
                 new MySqlServerVersion(new Version(8, 0, 25))))
                .BuildServiceProvider();

            
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MovieDbContext>();

                var newMovie = new Movie
                {
                    Movie_name = "Shutter Island",
                    Movie_genre = "Thriller",
                    Movie_duration = 138,
                    Movie_budget = 65000000
                };
                context.Movies.Add(newMovie);

                var newActor = new Actor
                {
                    Actor_name = "Leonardo DiCaprio",
                    Actor_birthday = new DateTime(1974, 11, 11),
                    Actor_picture = ""
                };
                context.Actors.Add(newActor);

                context.SaveChanges();
            }

            
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MovieDbContext>();

                var movies = context.Movies.ToList();
                var actors = context.Actors.ToList();

                Console.WriteLine("Películas:");
                foreach (var movie in movies)
                {
                    Console.WriteLine($"- {movie.Movie_name} ({movie.Movie_genre}, {movie.Movie_duration} min, {movie.Movie_budget:C})");
                }

                Console.WriteLine("\nActores:");
                foreach (var actor in actors)
                {
                    Console.WriteLine($"- {actor.Actor_name} ({actor.Actor_birthday.ToShortDateString()})");
                }
            }

        }
    }
}