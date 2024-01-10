using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Sheraton Hotel" },
                new Hotel { Id = 2, Name = "Helnan Hotel" },
                new Hotel { Id = 3, Name = "Tolip Hotel" }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Double Room See View", Price = 200, HotelId = 1 },
                new Room { Id = 2, Name = "Single Room See View", Price = 150, HotelId = 1 },
                new Room { Id = 3, Name = "Double Room City View", Price = 170, HotelId = 1 },
                new Room { Id = 4, Name = "Single Room City View", Price = 120, HotelId = 1 },

                new Room { Id = 5, Name = "Double Room Garden View", Price = 100, HotelId = 2 },
                new Room { Id = 6, Name = "Single Room Garden View", Price = 90, HotelId = 2 },
                new Room { Id = 7, Name = "Double Room Pool View", Price = 120, HotelId = 2 },
                new Room { Id = 8, Name = "Single Room Pool View", Price = 110, HotelId = 2 },

                new Room { Id = 9, Name = "Double Room See Standard", Price = 80, HotelId = 3 },
                new Room { Id = 10, Name = "Single Room See Standard", Price = 70, HotelId = 3 },
                new Room { Id = 11, Name = "Double Room City Deluxe", Price = 100, HotelId = 3 },
                new Room { Id = 12, Name = "Single Room City Deluxe", Price = 150, HotelId = 3 }
            );

        }
    }
}

