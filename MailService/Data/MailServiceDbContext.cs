using MailService.Models;
using Microsoft.EntityFrameworkCore;

namespace MailService.Data
{
    public class MailServiceDbContext : DbContext
    {
        public DbSet<MailQueueItem> MailQueue { get; set; }

        public MailServiceDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MailQueueItem>().ToTable("MailQueue");
        }
    }
}