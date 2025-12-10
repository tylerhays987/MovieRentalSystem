using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        public User(int id, string name)
        {
            UserId = id;
            Name = name;
        }
    }
}