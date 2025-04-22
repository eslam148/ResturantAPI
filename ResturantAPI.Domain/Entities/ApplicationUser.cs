 
using Microsoft.AspNetCore.Identity;

namespace ResturantAPI.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
       
        public List<Address> Addresses { get; set; } = new();
        public List<Order> Orders { get; set; } = new();

        public Restaurant? RestaurantOwnerProfile { get; set; }
        public Delivery? DeliveryDriverProfile { get; set; }
    }
}
