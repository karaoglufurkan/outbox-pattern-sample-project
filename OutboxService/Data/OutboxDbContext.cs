using Microsoft.EntityFrameworkCore;
using OutboxService.Models;
using Shared.Models;

namespace OutboxService.Data
{
    public class OutboxDbContext : DbContext
    {
        public OutboxDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<OutboxEvent> OutboxEvents { get; set; }
    }
}