using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ResturantAPI.Domain.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus { Pending, Preparing, Shipped, Delivered, Cancelled }
    public enum PaymentMethod { Cash, Credit, Online }
    public class Order : Entity<int>
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }= OrderStatus.Pending;
        public PaymentMethod paymentMethod { get; set; } = PaymentMethod.Cash;
        public bool IsPayed { get; set; } = false;
        public string PaymentTransctionId { get; set; } = string.Empty;

        // Foreign keys
        [ForeignKey("Customer")]
        public int customerId { get; set; }
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }

        [ForeignKey("DeliveryAddress")]
        public int AddressId { get; set; }

        [ForeignKey("Delivery")]
        public int DeliveryId { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        
        public Address DeliveryAddress { get; set; }

        public ICollection<OrderItem> Items { get; set; } 
        
        public Delivery Delivery { get; set; }
    }
}
