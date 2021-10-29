using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MortgageContext : DbContext
    {
        public DbSet<Mortgage> Mortgages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                Environment.GetEnvironmentVariable("Account"),
                Environment.GetEnvironmentVariable("ACCOUNT_KEY"),
                databaseName: "Houses");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mortgage>(e =>
            {
                e.ToContainer("MortgageContainer");
                e.HasKey(u => u.id);
                e.HasNoDiscriminator();
                e.HasPartitionKey(u => u.ZipCode);
                e.UseETagConcurrency();
            });
        }
    }
}
