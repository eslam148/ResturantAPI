using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Dtos
{
   public class RestaurantDto
    {
        public int? Id { get; set; } 
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsOpen { get; set; }
        public string  ImageUrl { get; set; }

    }
}
