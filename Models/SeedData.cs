using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HotelManagement.Data;
using System;
using System.Linq;

namespace HotelManagement.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new HotelContext(serviceProvider.GetRequiredService<DbContextOptions<HotelContext>>()))
            {
                // Check if the specific employees already exist in the database to avoid duplication
                if (context.Staff.Any(e => e.Name == "John" && e.Surname == "Doe"))
                {
                    return; // DB has been seeded with these specific employees
                }

                context.Staff.AddRange(
                    new Employee
                    {
                        Name = "John",
                        Surname = "Doe",
                        Age = 30,
                        Title = "Manager",
                        Address = "123 Main St"
                    },
                    new Employee
                    {
                        Name = "Jane",
                        Surname = "Smith",
                        Age = 25,
                        Title = "Receptionist",
                        Address = "456 Elm St"
                    },
                    new Employee
                    {
                        Name = "Michael",
                        Surname = "Johnson",
                        Age = 40,
                        Title = "Chef",
                        Address = "789 Pine St"
                    },
                    new Employee
                    {
                        Name = "Emily",
                        Surname = "Williams",
                        Age = 35,
                        Title = "Housekeeper",
                        Address = "101 Maple Ave"
                    }
                );

                context.SaveChanges(); // Save the seeded data to the database
            }
        }
    }
}