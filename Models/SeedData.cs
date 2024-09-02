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
            // Create a new scope to create a context from the service provider
            using (var context = new HotelContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<HotelContext>>()))
            {
                Console.WriteLine("Seeding data..."); // Log for debugging purposes

                // Check if any employees already exist
                if (context.Employee.Any())
                {
                    Console.WriteLine("Employees already exist. Seeding skipped."); // Logging
                    return; // DB has been seeded with employees, so return early
                }

                // Seed data
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

                // Save the changes to the database
                context.SaveChanges();
                Console.WriteLine("Seeding completed."); // Log after seeding
            }
        }
    }
}
