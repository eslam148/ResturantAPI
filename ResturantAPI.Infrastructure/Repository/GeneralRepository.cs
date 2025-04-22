using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResturantAPI.Domain;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Infrastructure.Context;

namespace ResturantAPI.Infrastructure.Repository
{
    public class GeneralRepository<T, TId> : IGeneralRepository<T, TId>
                where T : Entity<TId>
                where TId : IEquatable<TId>
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<T> _dbSet;

        public GeneralRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();

        }
        public   IQueryable<T> GetAllAsync(bool track = false)
        {
            if (track)
            {
                return _dbSet;
            }
            else
            {
                return _dbSet.AsNoTracking();
            }
        }
        public  IQueryable<T> GetAllAsync(string[] include, bool track = false)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }
            if (track)
            {
                return   query;
            }
            else
            {
                return  query.AsNoTracking();
            }
        }
        public   async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public   async Task<T> GetByIdAsync(object id, bool track = false)
        {
            if (track)
            {
                return await _dbSet.FindAsync(id);
            }
            else
            {
                return await _dbSet.AsNoTracking().FirstAsync(e => e.Id.Equals(id));
            }
        }
        public   async Task<T> GetByIdAsync(int id, string[]? include = default, bool track = false)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }
            if (track)
            {
                return await query.FirstAsync(e => e.Id.Equals(id));
            }
            else
            {
                return await query.AsNoTracking().FirstAsync(e => e.Id.Equals(id));
            }
        }
        public   async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public   void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public   void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public   T Filter(Expression<Func<T, bool>> predicate, bool track = false)
        {
            if (track)
            {
                return _dbSet.FirstOrDefault(predicate);
            }
            else
            {
                return _dbSet.AsNoTracking().FirstOrDefault(predicate);
            }
        }

        public   T Filter(Expression<Func<T, bool>> predicate, string[] include, bool track = false)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }
            if (track)
            {
                return query.FirstOrDefault(predicate);
            }
            else
            {
                return query.AsNoTracking().FirstOrDefault(predicate);
            }
        }

        public IQueryable<T> FilterAll(Expression<Func<T, bool>> predicate, bool track = false)
        {
            if (track)
            {
                return _dbSet.Where(predicate);

            }
            else
            {
                return _dbSet.Where(predicate).AsNoTracking();

            }
        }

        public IQueryable<T> FilterAll(Expression<Func<T, bool>> predicate, string[] include, bool track = false)
        {
            IQueryable<T> query = _dbSet;

            if (!track)
                query = query.AsNoTracking();

            // Apply includes
            if (include != null)
            {
                foreach (var includeProperty in include)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.Where(predicate);
        }

        public async Task<PagedResult<T>> GetPaginatedAsync(int pageNumber,int pageSize,Expression<Func<T, object>>? orderExpression = default)
        {
            var query = _context.Set<T>().AsNoTracking();

            int totalCount = await query.CountAsync();

            if (orderExpression is not null)
            {
                query = query.OrderBy(orderExpression);
            }

          
            IQueryable<T> items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
                

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

    
}
