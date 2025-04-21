using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync();

    }
}
