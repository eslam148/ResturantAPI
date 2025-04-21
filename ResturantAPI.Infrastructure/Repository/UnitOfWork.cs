using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Infrastructure.Context;

namespace ResturantAPI.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        //private IUserRepository _user;
        //private IRepository<ServiceCategory, int> _serviceCategories;
        //private IRepository<SubCategory, int> _subCategories;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }


        //public IRepository<ServiceCategory, int> ServiceCategories
        //{
        //    get
        //    {
        //        if (_serviceCategories is null)
        //        {
        //            _serviceCategories = new Repository<ServiceCategory, int>(_context);
        //        }
        //        return _serviceCategories;
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
