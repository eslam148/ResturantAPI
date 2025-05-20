using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Dtos
{
    public class PaymentDTO
    {

        [Required(ErrorMessage = "Credit card number is required.")]
        [RegularExpression(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|3[47][0-9]{13}|6(?:011|5[0-9]{2})[0-9]{12})$",
            ErrorMessage = "Invalid credit card number. Must be a valid Visa, MasterCard, Amex, or Discover card number.")]
        public string CreditCardNumber { get; set; } 
        public string ExpirationDate { get; set; }
    }
}
