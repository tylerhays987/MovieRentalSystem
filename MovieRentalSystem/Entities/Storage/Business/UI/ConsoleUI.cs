using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MovieRental.Business;

namespace MovieRental.UI
{
    public class ConsoleUI
    {
        private readonly MovieRentalSystem system;

        public ConsoleUI(MovieRentalSystem system)
        {
            this.system = system;
        }

        public void Run()
        {
            while (true)
            {
                ShowMenu();
                Console.Write("\nEnter your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewMovies(); break;
                    case "2": SearchMovie(); break;
                    case "3": RentMovie(); break;
                    case "4": ReturnMovie(); break;
                    case "5": ViewHistory(); break;
                    case "6":
                        Console.WriteLine("Exiting... goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice.\n");
                        break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n============================");
            Console.WriteLine("      Movie Rental System   ");
            Console.WriteLine("============================");
            Console.WriteLine("1. View all movies");
            Console.WriteLine("2. Search movie by title");
            Console.WriteLine("3. Rent a movie");
            Console.WriteLine("4. Return a movie");
            Console.WriteLine("5. View rental history");
            Console.WriteLine("6. Exit");
        }

        private void ViewMovies()
        {
            Console.WriteLine("\n--- Movies List ---");
            foreach (var m in system.GetAllMovies())
                Console.WriteLine(m);
        }

        private void SearchMovie()
        {
            Console.Write("\nEnter movie title: ");
            var title = Console.ReadLine() ?? "";

            var results = system.SearchMovies(title);

            if (results.Count == 0)
                Console.WriteLine("No results found.");
            else
                results.ForEach(r => Console.WriteLine(r));
        }

        private void RentMovie()
        {
            Console.Write("\nEnter movie ID to rent: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            if (system.RentMovie(id, 1)) // using UserId = 1
                Console.WriteLine("Movie rented successfully.");
            else
                Console.WriteLine("Cannot rent that movie.");
        }

        private void ReturnMovie()
        {
            Console.Write("\nEnter movie ID to return: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            if (system.ReturnMovie(id, 1))
                Console.WriteLine("Movie returned successfully.");
            else
                Console.WriteLine("You have not rented that movie.");
        }

        private void ViewHistory()
        {
            Console.WriteLine("\n--- Rental History ---");
            var list = system.GetRentalHistory(1);

            if (!list.Any())
                Console.WriteLine("No rental history.");
            else
                list.ForEach(r => Console.WriteLine(r));
        }
    }
}