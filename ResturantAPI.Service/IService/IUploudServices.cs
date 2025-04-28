using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ResturantAPI.Services.IService
{
    public interface IUploudServices
    {

        Task<string> UploadImageToAzureAsync(IFormFile file);

    }
}
