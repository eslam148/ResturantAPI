using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Domain.Entities
{
    public class OrderItem : Entity<int>
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }

        public Order Order { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}
