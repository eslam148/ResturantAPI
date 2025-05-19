using ResturantAPI.Domain.Entities;
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
         IGeneralRepository<Delivery, int> DeliveryRepository { get; }
          ICustomerRepository CustomerRepository { get; }
         IGeneralRepository<Restaurant, int> RestaurantRepository { get; }
         IOrderRepository OrderRepository {  get; }
         IGeneralRepository<Address, int> AddressRepository { get; }

         Task<int> SaveAsync();
    }   
}
