using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiningGuideRemake.Models;

namespace DiningGuideRemake.Data
{
    public class DiningGuideData : DbContext
    {
        public DiningGuideData(DbContextOptions<DiningGuideData> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Restaurant>().ToTable("Restaurants");
            modelBuilder.Entity<Rate>().ToTable("Rates");
        }
    }
}
