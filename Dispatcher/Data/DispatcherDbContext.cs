using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Dispatcher.Data
{
    public class DispatcherDbContext : DbContext
    {
        public DbSet<OutboxEvent> OutboxEvents { get; set; }

        public DispatcherDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OutboxEvent>().ToTable("OutboxEvents");
        }
    }
}