using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Infrastructure.Context;


namespace ResturantAPI.Infrastructure.Repository
{
    public class RestaurantRepository : GeneralRepository<Restaurant, int> ,  IRestaurantRepository
    {
      



        public RestaurantRepository(DatabaseContext _context ):base(_context)
        {
   
        }

    }
}
