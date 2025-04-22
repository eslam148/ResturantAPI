
using ResturantAPI.API.Configuration;
using ResturantAPI.Infrastructure.Repository;
using ResturantAPI.Services.IRepository;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Service;
using ResturantAPI.API.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ResturantAPI.Infrastructure.DataSeed;
namespace ResturantAPI.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
       
            builder.Services.ConfigureIdentity(builder.Configuration);
            builder.Services.ConfigureExtinction(builder.Configuration);
            builder.Services.ConfigureJwtToken(builder.Configuration);
             // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.ConfigureSwagger(builder.Configuration);
            var app = builder.Build();
           // app.InitializeDb();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
