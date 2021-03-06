using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using Shared.Models;

namespace OrderService.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OutboxEvent> OutboxEvents { get; set; }

        public OrderDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OutboxEvent>().ToTable("OutboxEvents");
            modelBuilder.Entity<Order>().ToTable("Orders");
        }
    }
}