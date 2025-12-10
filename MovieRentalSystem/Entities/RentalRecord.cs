using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Entities
{
    public class RentalRecord
    {
        public int RentalId { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public DateTime RentedOn { get; set; }
        public DateTime? ReturnedOn { get; set; }

        public RentalRecord(int rentalId, int movieId, int userId)
        {
            RentalId = rentalId;
            MovieId = movieId;
            UserId = userId;
            RentedOn = DateTime.Now;
        }

        public override string ToString()
        {
            string returned = ReturnedOn == null ? "Not returned yet" : ReturnedOn.ToString();
            return $"Rental #{RentalId} | Movie: {MovieId} | User: {UserId} | Out: {RentedOn} | Back: {returned}";
        }
    }
}