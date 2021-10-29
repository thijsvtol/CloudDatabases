using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
               Environment.GetEnvironmentVariable("ACCOUNT"),
                Environment.GetEnvironmentVariable("ACCOUNT_KEY"),
                databaseName: "Houses");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToContainer("UserContainer");
                e.HasKey(u => u.id);
                e.HasNoDiscriminator();
                e.HasPartitionKey(u => u.ZipCode);
                e.UseETagConcurrency();
            }
            );
        }
    }
}
