using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Dtos
{
    public class OrderDetailsDto
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryName { get; set; }
        public string Address { get; set; }
        public string RestaurantName { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public decimal TotalPrice { get; set; }

        public class OrderItemDto
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }
    }
}
