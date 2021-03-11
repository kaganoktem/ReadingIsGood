using Microsoft.EntityFrameworkCore;
using ReadingIsGood.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Persistence
{
    public class ReadingIsGoodDbContext : DbContext
    {
        /// <summary>
        /// ReadingIsGood Code-First sınıfı
        /// </summary>
        /// <param name="options"></param>
        public ReadingIsGoodDbContext(DbContextOptions<ReadingIsGoodDbContext> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
            .HasMany(p => p.Orders)
            .WithOne().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Customer>().Property(p => p.Surname).IsRequired();
            modelBuilder.Entity<Customer>().Property(p => p.Username).IsRequired();
            modelBuilder.Entity<Customer>().Property(p => p.Address).IsRequired();
            modelBuilder.Entity<Customer>().Property(p => p.Password).IsRequired();

            modelBuilder.Entity<Order>()
                .HasOne(p => p.Customer)
                .WithMany(p => p.Orders)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<Order>().Property(p => p.CustomerId).IsRequired();
            modelBuilder.Entity<Order>().Property(p => p.BookId).IsRequired();
            modelBuilder.Entity<Order>().Property(p => p.OrderDate).IsRequired();

            modelBuilder.Entity<Customer>()
                .Navigation(b => b.Orders)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            modelBuilder.Entity<BooksStock>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<BooksStock>().Property(p => p.NumberofBooks).IsRequired();
            
            modelBuilder.Entity<BookDeliveryInformation>().Property(p => p.bookId).IsRequired();
            modelBuilder.Entity<BookDeliveryInformation>().Property(p => p.userId).IsRequired();
        }

        /// <summary>
        /// Müşteri tablosu
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Kitap teslimat tablosu
        /// </summary>
        public DbSet<BookDeliveryInformation> BookDeliveryInformations { get; set; }

        /// <summary>
        /// Kitap stok tablosu
        /// </summary>
        public DbSet<BooksStock> BooksStocks { get; set; }
    }
}
