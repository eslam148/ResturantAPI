using Microsoft.AspNetCore.Mvc.RazorPages;
using ResturantAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantAPI.Services.Dtos.ResturntReportDTO
{
    public class AllResturantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int OrderCounter { get; set; }
    }
}
