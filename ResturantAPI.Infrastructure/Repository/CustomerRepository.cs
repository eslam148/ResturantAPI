using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Infrastructure.Context;
using ResturantAPI.Services.Dtos;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IRepository;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;
using ResturantAPI.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Infrastructure.Repository
{
   public class CustomerRepository : GeneralRepository<Customer, int>, ICustomerRepository
    {

        public CustomerRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Customer?> GetByUserIdAsync(string userId, string[]? include = default, bool track = false)
        {
            IQueryable<Customer> query = _context.Customers;
            if(include != null)
            {
                foreach (string item in include)
                {
                    query = query.Include(item);
                }
            }
            if (track)
            {
                return await query.FirstOrDefaultAsync(c => c.UserId.Equals(userId));
            }
            else
            {
                return await query.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.UserId.Equals(userId));

            }
        }
    }
}
