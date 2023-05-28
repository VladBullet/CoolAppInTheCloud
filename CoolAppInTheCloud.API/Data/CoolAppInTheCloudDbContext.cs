using CoolAppInTheCloud.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CoolAppInTheCloud.Data
{
    public class CoolAppInTheCloudDbContext : DbContext
    {
        protected readonly IConfiguration _config;
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public CoolAppInTheCloudDbContext(IConfiguration config, DbContextOptions<CoolAppInTheCloudDbContext> options) : base(options) { _config = config; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "PeopleDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Person
            modelBuilder.Entity<Person>().HasKey(x => new { x.Name, x.Age, x.Country, x.City, x.EyeColor, x.RealHairColor });

            // User
            modelBuilder.Entity<User>().HasKey(x => x.Id);

           
        }
    }
}
