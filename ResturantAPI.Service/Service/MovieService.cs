using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IRepository;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;

namespace ResturantAPI.Services.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;

        public MovieService(IMovieRepository _movieRepository)
        {
            movieRepository = _movieRepository;
        }
        public Response<List<Movies>> GetAllMovies()
        {
            var movies = movieRepository.GetAllMovies();
            Response<List<Movies>> response = new Response<List<Movies>>
            {
                Data = movies,
                Status = ResponseStatus.Success,
                Message = "Movies retrieved successfully",

            };
            return response;
        }
    }
}
