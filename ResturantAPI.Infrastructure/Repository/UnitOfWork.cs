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
      


        private IRestaurantRepository _restaurantRepository ;

        public UnitOfWork(DatabaseContext context)
        {

            _context = context;
        //    RestaurantRepository = new RestaurantRepository(context);
        }
        public IRestaurantRepository RestaurantRepository
        {
            get
            {
                if (_restaurantRepository == null)
                {
                    _restaurantRepository = new RestaurantRepository(_context);
                }
                return _restaurantRepository;
            }
        }

        //private IUserRepository _user;
        //private IRepository<ServiceCategory, int> _serviceCategories;
        //private IRepository<ServiceCategory, int> _serviceCategories;


        //public IGeneralRepository<Restaurant, int> Resturant => throw new NotImplementedException();


       
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

        //public UnitOfWork(DatabaseContext context)
        //{
        //    _context = context;
        //}


        //public IRestaurantRepository RestaurantRepository
        //{
        //    get
        //    {
        //        if (_restaurantRepository is null)
        //        {
        //            _restaurantRepository = new RestaurantRepository;
        //        }
        //        return _restaurantRepository;
        //    }
        //}

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
