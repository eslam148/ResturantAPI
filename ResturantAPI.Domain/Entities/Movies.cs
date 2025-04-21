using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Domain.Entities
{
    public class Movies
    {
        public Movies()
        {
        }
        public Movies(int MovieId, string Name, int Cost)
        {
            this.MovieId = MovieId;
            this.Name = Name;
            this.Cost = Cost;
        }
        public int MovieId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Cost { get; set; }

    }
}
