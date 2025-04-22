using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Domain.Entities
{
    public class Address: Entity<int>
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }  
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
