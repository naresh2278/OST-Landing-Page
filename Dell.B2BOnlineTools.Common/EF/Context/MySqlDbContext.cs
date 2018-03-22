using System;
using Microsoft.EntityFrameworkCore;

namespace Dell.B2BOnlineTools.Common.EF.Context
{
    public abstract class MySqlDbContext : DbContext, IApplicationDbContext
    {
        private readonly string _connectionString;
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options, string connectionString) : base(options)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }        
    }
}
