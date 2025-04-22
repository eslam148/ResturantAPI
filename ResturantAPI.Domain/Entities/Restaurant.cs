using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Domain.Entities
{
    public class Restaurant
        : Entity<int>
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsOpen { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Menu>? Menus { get; set; } 
        public ICollection<Order>? Orders { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
