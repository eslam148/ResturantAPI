using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Infrastructure.Repository
{
    public class AddressRepository : GeneralRepository<Address, int>, IAddressRepository
    {
        public AddressRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
