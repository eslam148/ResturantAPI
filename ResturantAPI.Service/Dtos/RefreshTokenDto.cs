﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Dtos
{
    public class RefreshTokenDto
    {
        public string token { get; set; }
        public string refreshToken { get; set; }
    }
}
