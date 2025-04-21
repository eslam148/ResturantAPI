using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Domain.Entities;

namespace ResturantAPI.Services.IService
{public interface IMovieService
    {
        List<Movies> GetAllMovies();

    }
}
