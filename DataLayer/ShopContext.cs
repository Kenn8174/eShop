using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DataLayer
{
    public class ShopContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=eShopDB;Trusted_Connection=True;")
                .EnableSensitiveDataLogging(true)
                .UseLoggerFactory(new ServiceCollection()
                .AddLogging(builder => builder.AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information))
                    .BuildServiceProvider().GetService<ILoggerFactory>());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasKey(k => k.CompanyID);
            modelBuilder.Entity<Phone>().HasKey(k => k.PhoneID);
            modelBuilder.Entity<Photo>().HasKey(k => k.PhotoID);
            modelBuilder.Entity<OrderLine>().HasKey(k => new { k.PhoneID, k.OrderID });
            modelBuilder.Entity<Order>().HasKey(k => k.OrderID);

            modelBuilder.Entity<Order>().Property(p => p.OrderDate).HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Phone>().Property(p => p.Price).HasColumnType("decimal(30,2)");

            modelBuilder.Entity<Photo>()
                .HasOne(x => x.Phone)
                .WithOne(x => x.Photo)
                .HasForeignKey<Photo>(x => x.PhoneID);

            modelBuilder.Entity<Phone>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Phone)
                .HasForeignKey(x => x.CompanyID);

            modelBuilder.Entity<OrderLine>()
                .HasOne(x => x.Phone)
                .WithMany(x => x.OrderLines)
                .HasForeignKey(x => x.PhoneID);

            modelBuilder.Entity<OrderLine>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderLines)
                .HasForeignKey(x => x.OrderID);
        }
    }
}
