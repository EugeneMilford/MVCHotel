using HotelManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace HotelManagement.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new HotelContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<HotelContext>>()))
            {
                // Look for any Employees
                if (!context.Hotel.Any())
                {
                    return;   // DB has been seeded
                }
                context.Employee.AddRange(
                new Employee
                {
                    Name = "Henry",
                    Surname = "Davids",
                    Age = 52,
                    Title = "Manager",
                    Address = "Cape Town"
                },
                new Employee
                {
                    Name = "Sally",
                    Surname = "Abrams",
                    Age = 27,
                    Title = "Admin",
                    Address = "Cape Town"
                }
                );
                context.SaveChanges();
            }
        }
    }
}
