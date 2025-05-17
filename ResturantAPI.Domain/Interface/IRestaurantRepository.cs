using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Domain.Entities;

namespace ResturantAPI.Domain.Interface
{
    public interface IRestaurantRepository :IGeneralRepository<Restaurant, int >
    {

    }
}
