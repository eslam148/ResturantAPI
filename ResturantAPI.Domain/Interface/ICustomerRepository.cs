using ResturantAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Domain.Interface
{
    public interface ICustomerRepository : IGeneralRepository<Customer,int>
    {
        Task<Customer?> GetByUserIdAsync(string userId, string[]? include = default, bool track = false); 
    }
}
