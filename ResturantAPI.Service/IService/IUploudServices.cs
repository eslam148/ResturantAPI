using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ResturantAPI.Services.Model;

namespace ResturantAPI.Services.IService
{
    public interface IUploudServices
    {

        Task<Response<string>> UploadImageAsync(IFormFile file);

    }
}
