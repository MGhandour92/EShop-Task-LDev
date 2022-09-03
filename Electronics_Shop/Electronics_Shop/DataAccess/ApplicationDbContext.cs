using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Electronics_Shop.Models;

namespace Electronics_Shop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "TVs" },
                new Category { Id = 2, Name = "Laptops" },
                new Category { Id = 3, Name = "Sound Systems" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Samsung 32", Description = "Samsung TV 32 inch", UnitPrice = 2500, DisountToApply = 10, CategoryId = 1 },
                new Product { Id = 2, Name = "LG 32", Description = "LG TV 32 Inch Satellite", UnitPrice = 3000, DisountToApply = 20, CategoryId = 1 },
                new Product { Id = 3, Name = "Toshiba 49", Description = "Toshiba 49 inch oled", UnitPrice = 5000, DisountToApply = 0, CategoryId = 1 },
                new Product { Id = 4, Name = "Toshiba 64", Description = "Toshiba 64 inch QLED", UnitPrice = 9000, DisountToApply = 15, CategoryId = 1 },
                new Product { Id = 5, Name = "LG 50", Description = "LG 50 inch QLED", UnitPrice = 10000, DisountToApply = 30, CategoryId = 1 },
                new Product { Id = 6, Name = "Samsung 65", Description = "Samsung 65 QLED 4K", UnitPrice = 15000, DisountToApply = 25, CategoryId = 1 },
                new Product { Id = 7, Name = "HP Probook", Description = "HP Probook 15 inch i7 G3", UnitPrice = 11000, DisountToApply = 10, CategoryId = 2 },
                new Product { Id = 8, Name = "Dell Latitude", Description = "Dell Latitude 15 inch i5", UnitPrice = 9500, DisountToApply = 0, CategoryId = 2 },
                new Product { Id = 9, Name = "Lenovo Thinkpad", Description = "Lenovo Thinkpad 15 inch i5", UnitPrice = 9000, DisountToApply = 15, CategoryId = 2 },
                new Product { Id = 10, Name = "Dell Vostro", Description = "Dell Vostro 15 inch FHD i7", UnitPrice = 10500, DisountToApply = 0, CategoryId = 2 },
                new Product { Id = 11, Name = "Yamaha", Description = "YHT-4950U 4K Ultra HD 5.1-Channel Home Theater System with Bluetooth", UnitPrice = 10800, DisountToApply = 5, CategoryId = 3 },
                new Product { Id = 12, Name = "Sony", Description = "Sony Complete 8 Speaker System- SSCS3 (2), SSCS5, SSCS8, SACS9, SSCSE", UnitPrice = 25000, DisountToApply = 15, CategoryId = 3 },
                new Product { Id = 13, Name = "JBL", Description = "JBL Professional EON208P Portable All-in-One 2-way PA System with 8-Channel Mixer and Bluetooth", UnitPrice = 16000, DisountToApply = 0, CategoryId = 3 },
                new Product { Id = 14, Name = "Logitech", Description = "Logitech Z906 5.1 Surround Sound Speaker System - THX, Dolby Digital and DTS Digital Certified - Black", UnitPrice = 8000, DisountToApply = 5, CategoryId = 3 },
                new Product { Id = 15, Name = "Klipsch", Description = "Klipsch Black Reference Theater Pack 5.1 Surround Sound System", UnitPrice = 6000, DisountToApply = 10, CategoryId = 3 },
                new Product { Id = 16, Name = "Earthquake", Description = "Earthquake Sound DJ-Array Gen2 4x4 Line Array Loudspeaker System, Set of 2", UnitPrice = 8750, DisountToApply = 0, CategoryId = 3 }
            );
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderLine> OrderLine { get; set; }
    }
}
