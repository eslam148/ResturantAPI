using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Domain.Entities;

namespace ResturantAPI.Services.IRepository
{
    public interface IMovieRepository
    {
        List<Movies> GetAllMovies();

    }
}
