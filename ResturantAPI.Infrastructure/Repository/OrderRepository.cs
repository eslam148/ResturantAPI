using Microsoft.EntityFrameworkCore;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Infrastructure.Repository
{
    public class OrderRepository:GeneralRepository<Order,int>, IOrderRepository
    {
        public OrderRepository(DatabaseContext context):base(context) { }
     
        public async Task<IEnumerable<Order>> GetAllIncludingAsync(params Expression<Func<Order, object>>[] includes)
        {
            IQueryable<Order> query = _context.Orders;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

    }
}
