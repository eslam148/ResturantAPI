﻿using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using ResturantAPI.Domain.Interface;
using System.IO.Abstractions;
using ResturantAPI.Infrastructure.DataSeed;
using System.Security.Cryptography;
using ResturantAPI.Domain.Entities;
using ResturantAPI.Domain;


namespace ResturantAPI.Infrastructure.Context
{
    public  class DbInitializer: IDbInitializer
    {
        private readonly DatabaseContext _db;
        private readonly IFileSystem _fileSystem;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(DatabaseContext db, IFileSystem fileSystem, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _fileSystem = fileSystem;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            _db.Database.Migrate();
        }

        public void SeedData()
        {
            var basePath = AppContext.BaseDirectory;
            SeedIdentityDataAsync().Wait();

            SeedDbSet(_db, _db.Addresses, Path.Combine("SeedFiles", "Address.json"));
            SeedDbSet(_db, _db.Restaurants, Path.Combine("SeedFiles", "Restaurant.json"));
            SeedDbSet(_db, _db.Customers, Path.Combine("SeedFiles", "Customer.json"));
            SeedDbSet(_db, _db.Deliveries, Path.Combine("SeedFiles", "Delivery.json"));
            SeedDbSet(_db, _db.Menus, Path.Combine("SeedFiles", "Menu.json"));
            SeedDbSet(_db, _db.MenuItems, Path.Combine("SeedFiles", "MenuItem.json"));
            SeedDbSet(_db, _db.Orders, Path.Combine("SeedFiles", "Order.json"));
            SeedDbSet(_db, _db.OrderItems, Path.Combine("SeedFiles", "OrderItem.json"));
            SeedDbSet(_db, _db.Payments, Path.Combine("SeedFiles", "Payment.json"));
            _db.SaveChanges();
        }
        private void SeedDbSet<T>(DatabaseContext db, DbSet<T> dbSet, string file) where T : Entity<int>
        {
            var content = _fileSystem.File.ReadAllText(file, Encoding.UTF8);
            var values = DeserializeJson<T>(content);
            UpdateSeedData(db, dbSet, values);
        }
        //private void SeedDbSet<T>(DatabaseContext db, DbSet<T> dbSet, string file) where T : class, BaseEntity
        //{
        //    //if (!_fileSystem.File.Exists(file))
        //    //    return;

        //    var content = _fileSystem.File.ReadAllText(file, Encoding.UTF8);
        //    var hash = GetHash(content);

        //    //if (!CompareAndUpdateFileHash(db, file, hash))
        //    //{
        //        var values = DeserializeJson<T>(content);
        //        UpdateSeedData(db, dbSet, values);
        //  //  }
        //}

        private static byte[] GetHash(string content)
        {
            byte[] contentsBytes = Encoding.UTF8.GetBytes(content);
            using var sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(contentsBytes);
            return hash;
        }
 
        private static IEnumerable<T> DeserializeJson<T>(string content)
        {
            var empty = new List<T>();

            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<IEnumerable<T>>(content, options) ?? empty;
        }
        private static void UpdateSeedData<T>(DatabaseContext db, DbSet<T> dbSet, IEnumerable<T> values) where T : Entity<int>
        {
            foreach (var value in values)
            {
                var exists = dbSet.AsNoTracking().Any(e => e.Id == value.Id);

                if (!exists)
                {
                    dbSet.Add(value);
                }
               
            }

            db.SaveChanges();
        }
        //private static void UpdateSeedData<T>(DatabaseContext db, DbSet<T> dbSet, IEnumerable<T> values) where T : class, Entity
        //{
        //    foreach (var value in values)
        //    {
        //        var exisitingEntity = dbSet.Find(value.Id);

        //        if (exisitingEntity is null)
        //            db.Add(value);
        //        else
        //            db.Entry(exisitingEntity).CurrentValues.SetValues(value);
        //    }
        //}

        private async Task SeedIdentityDataAsync()
        {
            string[] roles = { "Admin", "Delievary", "Resturant", "Customer" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Admin
            var adminEmail = "admin1@example.com";
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    Id = "267ca861-4093-4692-ae35-942d9995b170",
                    Name = "Admin",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Delievary
            var deliveryEmail = "Delievary@example.com";
            var deliveryUser = await _userManager.FindByEmailAsync(deliveryEmail);

            if (deliveryUser == null)
            {
                deliveryUser = new ApplicationUser
                {
                    Id = "0c5a0be0-aa43-4cea-9875-154f9a1ccb57",
                    Name = "Delievary",
                    UserName = deliveryEmail,
                    Email = deliveryEmail,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(deliveryUser, "Admin@123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(deliveryUser, "Delievary");
                }
            }

            // Resturant
            var resturantEmail = "Resturant@example.com";
            var resturantUser = await _userManager.FindByEmailAsync(resturantEmail);

            if (resturantUser == null)
            {
                resturantUser = new ApplicationUser
                {
                    Id = "438d2f1a-ba84-47a7-9f00-54752f9c1afa",
                    Name = "Resturant",
                    UserName = resturantEmail,
                    Email = resturantEmail,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(resturantUser, "Admin@123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(resturantUser, "Resturant");
                }
            }

            // Customer
            var customerEmail = "Customer@example.com";
            var customerUser = await _userManager.FindByEmailAsync(customerEmail);

            if (customerUser == null)
            {
                customerUser = new ApplicationUser
                {
                    Id= "4afa8048-9a2a-42ba-b997-c15b344570a4",
                    Name = "Customer",
                    UserName = customerEmail,
                    Email = customerEmail,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(customerUser, "Admin@123");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(customerUser, "Customer");
                }
            }
        }




    }


}
