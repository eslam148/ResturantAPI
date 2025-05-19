using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Dtos.ReportDTO
{
    public class AllCustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int OrderCounter { get; set; }
    }
}
