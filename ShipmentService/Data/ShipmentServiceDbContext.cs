using Microsoft.EntityFrameworkCore;
using ShipmentService.Models;

namespace ShipmentService.Data
{
    public class ShipmentServiceDbContext : DbContext
    {
        public DbSet<Shipment> Shipments { get; set; }

        public ShipmentServiceDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipment>().ToTable("Shipments");
        }
    }
}