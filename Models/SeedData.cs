using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using System;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>());

            // Poista kaikki elokuvat
            context.Movie.RemoveRange(context.Movie);
            context.SaveChanges();

            // Määrittele mahdolliset genret
            string[] genres = { "Action", "Comedy", "Drama", "Horror", "Sci-Fi", "Romance", "Thriller", "Animation" };

            // Luo satunnaisgeneroija
            Random random = new Random();

            // Lisää 100 elokuvaa
            for (int i = 1; i <= 100; i++)
            {
                string genre = genres[random.Next(genres.Length)];
                decimal price = (decimal)(random.Next(5, 21) + random.NextDouble());
                DateTime releaseDate = new DateTime(
                    random.Next(1980, 2023), // Satunnainen vuosi
                    random.Next(1, 13),     // Satunnainen kuukausi
                    random.Next(1, 28));    // Satunnainen päivä

                string rating = random.NextDouble() > 0.5 ? "PG-13" : "R";

                context.Movie.Add(new Movie
                {
                    Title = $"Movie {i}",
                    Genre = genre,
                    Price = Math.Round(price, 2),
                    ReleaseDate = releaseDate,
                    Rating = rating
                });
            }

            // Tallenna tietokantaan
            context.SaveChanges();
        }
    }
}
