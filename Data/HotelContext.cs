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
    }
}
