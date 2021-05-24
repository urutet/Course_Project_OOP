using JParts.MVVM.Model;
using JParts.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JParts.DBContext
{
    public class JPartsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("Data Source=DESKTOP-NTCQFP9;Initial Catalog=JParts;Integrated Security=True");
        }

        public virtual DbSet<Address> Adresses { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<PartsOrders> PartsOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PartsOrders>()
                .HasKey(k => new { k.PartID, k.OrderID });

            modelBuilder
                .Entity<PartsOrders>()
                .HasOne(k => k.Part)
                .WithMany(k => k.PartsOrders);

            modelBuilder
                .Entity<PartsOrders>()
                .HasOne(k => k.Order)
                .WithMany(k => k.PartsOrders);
        }

    }
}
