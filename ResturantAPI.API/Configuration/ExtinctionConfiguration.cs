using System.IO.Abstractions;
using Microsoft.AspNetCore.Hosting;
using RestaurantAPI.Services;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Infrastructure.Context;
using ResturantAPI.Infrastructure.Repository;
using ResturantAPI.Services.IRepository;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.MapperHelper;
using ResturantAPI.Services.Service;

namespace ResturantAPI.API.Configuration
{
    public static class ExtinctionConfiguration
    {
        public static void ConfigureExtinction(this IServiceCollection services, IConfiguration configuration)
        {
            // Register repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            // Register services
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IRestaurantService, RestaurantService>();

            // Register unit of work and file system
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFileSystem, FileSystem>();

            // Register DbInitializer
            services.AddScoped<DbInitializer>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            // Register HttpContextAccessor for AuthServices
            services.AddHttpContextAccessor();

            // Configure CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
        }
    }
}
