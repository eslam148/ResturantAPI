using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {

        // Define properties for your repositories here 

        IOrderRepository OrderRepository { get; }
        Task<int> SaveAsync();

    }
}
