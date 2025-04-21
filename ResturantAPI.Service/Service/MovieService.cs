using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.IRepository;
using ResturantAPI.Services.IService;

namespace ResturantAPI.Services.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;

        public MovieService(IMovieRepository _movieRepository)
        {
            movieRepository = _movieRepository;
        }
        public List<Movies> GetAllMovies()
        {
            var movies = movieRepository.GetAllMovies();

            return movies;
        }
    }
}
