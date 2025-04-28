using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Infrastructure.Context;

namespace ResturantAPI.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
      

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }


       
        private IGeneralRepository<Restaurant, int> restaurant;
        public IGeneralRepository<Restaurant, int> Restaurant
        {
            get
            {
                if (restaurant is null)
                {
                    restaurant = new GeneralRepository<Restaurant, int>(_context);
                }
                return restaurant;
            }
        }
        private IGeneralRepository<Customer, int> customer;
        public IGeneralRepository<Customer, int> Customer
        {
            get
            {
                if (customer is null)
                {
                    customer = new GeneralRepository<Customer, int>(_context);
                }
                return customer;
            }
        }
        private IGeneralRepository<Delivery, int> delivery;
        public IGeneralRepository<Delivery, int> Delivery
        {
            get
            {
                if (delivery is null)
                {
                    delivery = new GeneralRepository<Delivery, int>(_context);
                }
                return delivery;
            }
        }

     
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
