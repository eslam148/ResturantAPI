using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Domain.Interface
{
    public interface IGeneralRepository<T, TId>
        where T : Entity<TId>
        where TId : IEquatable<TId>
    {
         IQueryable<T> GetAllAsync(bool track = false);

        IQueryable<T> GetAllAsync(string[] include, bool track = false);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(object id, bool track = false);

        Task<T> GetByIdAsync(int id, string[]? include = default, bool track = false);

        Task AddAsync(T entity);
        void Update(T entity);

        void Delete(T entity);

        T Filter(Expression<Func<T, bool>> predicate, bool track = false);

        T Filter(Expression<Func<T, bool>> predicate, string[] include, bool track = false);

        IQueryable<T> FilterAll(Expression<Func<T, bool>> predicate, bool track = false);

        IQueryable<T> FilterAll(Expression<Func<T, bool>> predicate, string[] include, bool track = false);

         Task<PagedResult<T>> GetPaginatedAsync(int pageNumber,int pageSize,Expression<Func<T, object>>? orderExpression = default);
       
    
    }
}
