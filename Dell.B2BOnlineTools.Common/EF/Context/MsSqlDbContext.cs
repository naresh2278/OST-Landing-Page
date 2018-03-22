using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dell.B2BOnlineTools.Common.EF.Context
{
    public abstract class MsSqlDbContext : DbContext, IApplicationDbContext
    {
        private readonly string _connectionString;
        public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options, string connectionString) : base(options)
        {
            _connectionString = connectionString;
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
        }
    }
}
