

using Dell.B2BOnlineTools.Common.Batch.Models;
using Dell.B2BOnlineTools.Common.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace Dell.B2BOnlineTools.Common.Batch.Context.MySql
{
    public class AppDbContext : MySqlDbContext
    {
        public AppDbContext(DbContextOptions<MySqlDbContext> options, string connectionString) : base(options, connectionString)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("Batch");
            modelBuilder.Entity<BatchRegistry>()
                .HasKey(m => new { m.QueueName, m.BatchId });
            modelBuilder.Entity<BatchEmailAudit>()
                .HasKey(m => new { m.QueueName, m.BatchId, m.BatchIndex });
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<BatchRegistry> BatchRegistry { get; set; }
        public virtual DbSet<BatchEmailAudit> BatchEmailAudit { get; set; }
    }
}
