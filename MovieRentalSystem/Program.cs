using MovieRental.Storage;
using MovieRental.Business;
using MovieRental.UI;

class Program
{
    static void Main(string[] args)
    {
        FileStorage storage = new FileStorage();
        MovieRentalSystem system = new MovieRentalSystem(storage);
        ConsoleUI ui = new ConsoleUI(system);

        ui.Run();
    }
}