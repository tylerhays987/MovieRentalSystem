using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MovieRental.Entities;
using MovieRental.Storage;

namespace MovieRental.Business
{
    public class MovieRentalSystem
    {
        private readonly FileStorage storage;

        public List<Movie> Movies { get; private set; }
        public List<RentalRecord> Rentals { get; private set; }

        public MovieRentalSystem(FileStorage storage)
        {
            this.storage = storage;
            Movies = storage.LoadMovies();
            Rentals = storage.LoadRentals();

            // Seed sample movies if file is empty
            if (Movies.Count == 0)
            {
                Movies = new List<Movie>
                {
                    new Movie(1, "Inception"),
                    new Movie(2, "The Matrix"),
                    new Movie(3, "The Godfather"),
                    new Movie(4, "Toy Story"),
                };
                storage.SaveMovies(Movies);
            }
        }

        public List<Movie> GetAllMovies() => Movies;

        public List<Movie> SearchMovies(string title) =>
            Movies.Where(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

        public bool RentMovie(int movieId, int userId)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == movieId);

            if (movie == null || !movie.IsAvailable)
                return false;

            movie.IsAvailable = false;

            int nextId = Rentals.Count + 1;
            Rentals.Add(new RentalRecord(nextId, movieId, userId));

            storage.SaveMovies(Movies);
            storage.SaveRentals(Rentals);

            return true;
        }

        public bool ReturnMovie(int movieId, int userId)
        {
            var record = Rentals.FirstOrDefault(r =>
                r.MovieId == movieId && r.UserId == userId && r.ReturnedOn == null);

            if (record == null)
                return false;

            record.ReturnedOn = DateTime.Now;

            var movie = Movies.First(m => m.Id == movieId);
            movie.IsAvailable = true;

            storage.SaveMovies(Movies);
            storage.SaveRentals(Rentals);

            return true;
        }

        public List<RentalRecord> GetRentalHistory(int userId) =>
            Rentals.Where(r => r.UserId == userId).ToList();
    }
}