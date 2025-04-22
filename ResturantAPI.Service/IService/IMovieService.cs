using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Services.Model;

namespace ResturantAPI.Services.IService
{public interface IMovieService
    {
        Response<List<Movies>> GetAllMovies();

    }
}
