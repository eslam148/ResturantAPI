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
