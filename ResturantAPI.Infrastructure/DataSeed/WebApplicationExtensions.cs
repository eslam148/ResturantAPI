using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ResturantAPI.Infrastructure.Context;

namespace ResturantAPI.Infrastructure.DataSeed
{
    public static class WebApplicationExtensions
    {
        public static void InitializeDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetService<DbInitializer>();

            if (dbInitializer is not null)
            {
                dbInitializer.Initialize();
                dbInitializer.SeedData();
            }
        }
    }
}
