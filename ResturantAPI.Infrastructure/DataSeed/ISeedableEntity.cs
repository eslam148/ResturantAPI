﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Infrastructure.Context
{
    public interface ISeedableEntity
    {
        public Guid Id { get; }

    }
}
