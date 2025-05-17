using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Domain.Entities
{
    public class Customer : Entity<int>
    {


        [ForeignKey("User")]
        public string UserId { get; set; }
       
        public ApplicationUser User { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
