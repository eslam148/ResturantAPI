using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Infrastructure.Context;
using ResturantAPI.Services.IRepository;
using ResturantAPI.Services.IService;

namespace ResturantAPI.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;


        private ICustomerRepository _customer;
        //private IUserRepository _user;
        //private IRepository<ServiceCategory, int> _serviceCategories;
        //private IRepository<SubCategory, int> _subCategories;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;

        }

        public ICustomerRepository Customer
        {
            get
            {
                if(_customer is null)
                {
                    _customer = new CustomerRepository(_context);
                }

                return _customer;
            }
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

        private IGeneralRepository<Address, int> address;
        public IGeneralRepository<Address, int> Address
        {
            get
            {
                if (address is null)
                {
                    address = new GeneralRepository<Address, int>(_context);
                }
                return address;
            }
        }


        //public IUserRepository Users
        //{
        //    get
        //    {
        //        if (_user is null)
        //        {
        //            _user = new UserRepository(_context);
        //        }
        //        return _user;
        //    }
        //}

        // public IRepository<SubCategory, int> SUbCategories
        //{
        //    get
        //    {
        //        if (_subCategories is null)
        //        {
        //            _subCategories = new Repository<SubCategory, int>(_context);
        //        }
        //        return _subCategories;
        //    }
        //}

        public IOrderRepository OrderRepository
        {
            get
            {
                if (Order is null)
                {
                    Order = new OrderRepository(_context);
                }
                return Order;
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
