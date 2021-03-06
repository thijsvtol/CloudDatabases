using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class HouseContext : DbContext
    {
        public DbSet<House> Houses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
               Environment.GetEnvironmentVariable("ACCOUNT"),
                Environment.GetEnvironmentVariable("ACCOUNT_KEY"),
                databaseName: "Houses");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<House>(e =>
            {
                e.ToContainer("HouseContainer");
                e.HasKey(u => u.id);
                e.HasNoDiscriminator();
                e.HasPartitionKey(u => u.ZipCode);
                e.UseETagConcurrency();
            }
            );
        }
    }
}
