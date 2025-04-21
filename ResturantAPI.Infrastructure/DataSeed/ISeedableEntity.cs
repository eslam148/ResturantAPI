using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Infrastructure.DataSeed
{
    public interface ISeedableEntity
    {
        public Guid Id { get; }

    }
}
