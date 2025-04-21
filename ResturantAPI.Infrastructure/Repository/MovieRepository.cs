using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.IRepository;

namespace ResturantAPI.Infrastructure.Repository
{
    public class MovieRepository:IMovieRepository
    {
        private static List<Movies> movies = new List<Movies>()
        {
            new Movies() { Cost=100,Name="M1",MovieId=1},
            new Movies() { Cost=200,Name="M2",MovieId=2},
            new Movies() { Cost=150,Name="M3",MovieId=3},
        };

        public List<Movies> GetAllMovies()
        {
            return movies;

        }
    }
}
