using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiProject.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }
} 