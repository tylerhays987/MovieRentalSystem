using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsAvailable { get; set; }

        public Movie(int id, string title, bool isAvailable = true)
        {
            Id = id;
            Title = title;
            IsAvailable = isAvailable;
        }

        public override string ToString()
        {
            string status = IsAvailable ? "Available" : "Rented";
            return $"{Id}. {Title} - {status}";
        }
    }
}