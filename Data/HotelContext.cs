using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;

namespace HotelManagement.Data
{
    public class HotelContext : DbContext
    {
        public HotelContext (DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        public DbSet<Hotel> Hotel { get; set; }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Activities> Activities { get; set; } 

        public DbSet<Cinema> Cinema { get; set; }

        public DbSet<Restaurant> Restaurant { get; set; }

        public DbSet<PlayArea> Kids { get; set; }
        public DbSet<Spa> Spabooking { get; set; }

        public DbSet<Contact> ContactMessages { get; set; }
    }
}
