using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Kit.Data.DatabaseLogic
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Main");            

            //Register EntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DatabaseContext)));
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
