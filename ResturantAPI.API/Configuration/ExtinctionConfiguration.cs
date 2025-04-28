using System.IO.Abstractions;
using ResturantAPI.Domain.Interface;
using ResturantAPI.Infrastructure.Context;
using ResturantAPI.Infrastructure.Repository;
using ResturantAPI.Services.IRepository;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Service;

namespace ResturantAPI.API.Configuration
{
    public static class ExtinctionConfiguration
    {
        public static void ConfigureExtinction(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IUploudServices, UploudServices>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<DbInitializer>();


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
