using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Dtos
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<AddressDTO> Addresses { get; set; }
        public List<PaymentDTO> Payments { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}
