using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Domain.Entities;


using ResturantAPI.Domain.Interface;



namespace ResturantAPI.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
       
        IRestaurantRepository RestaurantRepository { get; } 

        Task<int> SaveAsync();

    }
}
