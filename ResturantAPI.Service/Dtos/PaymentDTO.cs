using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Dtos
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public string CreditCardNumber { get; set; } 
        public string ExpirationDate { get; set; }
    }
}
