using CoolAppInTheCloud.Data.Models;
using CoolAppInTheCloud.Helpers_Extensions;

namespace CoolAppInTheCloud.Data
{
    public class MockDatabase
    {
        private static MockDatabase instance;
        private static readonly object lockObject = new object();

        public List<Person> People { get; set; }
        public List<User> Users { get; set; }

        private MockDatabase()
        {
            People = Helpers.ReadPeopleFromFile();
            Users = new List<User>
            {
                new User(){Username = "Admin", Password = "1a1dc91c907325c69271ddf0c944bc72" , Role = "Admin"}, // password = "pass"
                new User(){Username = "User", Password = "1a1dc91c907325c69271ddf0c944bc72" , Role = "User"}
            };
        }

        public static MockDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new MockDatabase();
                        }
                    }
                }
                return instance;
            }
        }
    }

}
