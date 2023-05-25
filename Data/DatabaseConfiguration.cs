using CoolAppInTheCloud.Data.Models;
using CoolAppInTheCloud.Helpers_Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoolAppInTheCloud.Data
{
    public class DatabaseConfiguration
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // User
            var userList = new List<User>
            {
                new User(){Username = "Admin", Password = "1a1dc91c907325c69271ddf0c944bc72" , Role = "Admin"}, // password = "pass"
                new User(){Username = "User", Password = "1a1dc91c907325c69271ddf0c944bc72" , Role = "User"}
            };
            modelBuilder.Entity<User>().HasData(userList);

            // Person
            var personList = Helpers.ReadPeopleFromFile();
            modelBuilder.Entity<Person>().HasData(personList);
        }
    }
}