using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResturantAPI.Domain.Entities;

namespace ResturantAPI.Services.Dtos
{
    public class AddRestaurantDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsOpen { get; set; }
        public string ImageUrl { get; set; }
        public string? UserId { get; set; }
    }
}
