using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResturantAPI.Domain.Entities;

namespace ResturantAPI.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
         IGeneralRepository<Delivery, int> Delivery { get; }
         IGeneralRepository<Customer, int> Customer { get; }
         IGeneralRepository<Restaurant, int> Restaurant { get; }
        Task<int> SaveAsync();

    }
}
