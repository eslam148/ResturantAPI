using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Dtos
{
    public class AddressDTO
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
