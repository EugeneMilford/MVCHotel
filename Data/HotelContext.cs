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
        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Employee> Staff { get; set; }
        public DbSet<Activities> Activities { get; set; }
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<PlayArea> Kids { get; set; }
        public DbSet<Spa> Spabooking { get; set; }
        public DbSet<Contact> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");

            modelBuilder.Entity<Activities>()
        .HasOne(a => a.User)
        .WithMany() // Optional - if you want to have navigation properties for HotelUser to Activities
        .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Cinema>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<PlayArea>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Spa>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);
        }
    }
}
