using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MovieRental.Entities;

namespace MovieRental.Storage
{
    public class FileStorage
    {
        private readonly string movieFile = "movies.txt";
        private readonly string rentalFile = "rentals.txt";

        // ---------------- MOVIES ---------------- //
        public List<Movie> LoadMovies()
        {
            var movies = new List<Movie>();

            if (!File.Exists(movieFile))
                return movies;

            foreach (var line in File.ReadAllLines(movieFile))
            {
                var parts = line.Split('|');
                int id = int.Parse(parts[0]);
                string title = parts[1];
                bool available = bool.Parse(parts[2]);

                movies.Add(new Movie(id, title, available));
            }

            return movies;
        }

        public void SaveMovies(List<Movie> movies)
        {
            using var writer = new StreamWriter(movieFile);
            foreach (var m in movies)
                writer.WriteLine($"{m.Id}|{m.Title}|{m.IsAvailable}");
        }

        // ---------------- RENTALS ---------------- //
        public List<RentalRecord> LoadRentals()
        {
            var rentals = new List<RentalRecord>();
            if (!File.Exists(rentalFile))
                return rentals;

            foreach (var line in File.ReadAllLines(rentalFile))
            {
                var parts = line.Split('|');

                var record = new RentalRecord(
                    rentalId: int.Parse(parts[0]),
                    movieId: int.Parse(parts[2]),
                    userId: int.Parse(parts[1])
                )
                {
                    RentedOn = DateTime.Parse(parts[3]),
                    ReturnedOn = string.IsNullOrWhiteSpace(parts[4])
                        ? null
                        : DateTime.Parse(parts[4])
                };

                rentals.Add(record);
            }

            return rentals;
        }

        public void SaveRentals(List<RentalRecord> rentals)
        {
            using var writer = new StreamWriter(rentalFile);

            foreach (var r in rentals)
            {
                writer.WriteLine($"{r.RentalId}|{r.UserId}|{r.MovieId}|{r.RentedOn}|{r.ReturnedOn}");
            }
        }
    }
}