using JParts.MVVM.Model;
using JParts.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace JParts.DBContext
{
    public class JPartsContext : DbContext
    {
        public JPartsContext() : base("JPartsContext")
        {
        }

        public virtual DbSet<Address> Adresses { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }

    }
}
