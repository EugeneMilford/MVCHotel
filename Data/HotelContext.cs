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

        public DbSet<HotelManagement.Models.Hotel> Hotel { get; set; } = default!;

        public DbSet<HotelManagement.Models.Room> Room { get; set; }

        public DbSet<HotelManagement.Models.Employee> Employee { get; set; }

        public DbSet<HotelManagement.Models.Invoice> Invoice { get; set; }

        public DbSet<HotelManagement.Models.Activities> Activities { get; set; } 

        public DbSet<HotelManagement.Models.Cinema> Cinema { get; set; }
      
        public DbSet<HotelManagement.Models.Fitness> Fitness { get; set; }

        public DbSet<HotelManagement.Models.Restaurant> Restaurant { get; set; }

        public DbSet<HotelManagement.Models.PlayArea> Kids { get; set; }
        public DbSet<HotelManagement.Models.Spa> Spabooking { get; set; }
    }
}
