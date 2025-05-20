using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Dtos
{
    public class OrderDTO
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsPayed { get; set; }
        public int RestaurantId { get; set; }
        public int AddressId { get; set; }
        public int DeliveryId { get; set; }
    }
}
