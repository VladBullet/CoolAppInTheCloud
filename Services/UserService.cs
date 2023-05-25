using CoolAppInTheCloud.Data;
using CoolAppInTheCloud.Data.Models;
using CoolAppInTheCloud.Helpers_Extensions;
using Microsoft.EntityFrameworkCore;

namespace CoolAppInTheCloud.Services
{
    public interface IUserService
    {
        User GetUserById(int id);
        User GetUserByUsername(string username);
        IEnumerable<User> GetAllUsers();
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
    }

    public class UserService : IUserService
    {
        private readonly CoolAppInTheCloudDbContext _dbContext;
        private readonly MockDatabase _db = MockDatabase.Instance;

        public UserService(CoolAppInTheCloudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserById(int id)
        {
            return _db.Users?.FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _db.Users?.FirstOrDefault(u => u.Username == username);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users;
        }

        public bool CreateUser(User user)
        {
            try
            {
                if (_db.Users.Any(x => x.Id == user.Id))
                {
                    Console.WriteLine("User already exists!");
                    return false;
                }
                user.Password = user.Password.ToMd5();
                _db.Users.Add(user);
                // await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                if (GetUserById(user.Id).Password != user.Password)
                {
                    user.Password = user.Password.ToMd5(); // make sure the password gets hashed
                }

                var dbUser = GetUserById(user.Id);
                _db.Users.Remove(dbUser); // remove old user
                _db.Users.Add(user); // add new user
                // await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool DeleteUser(User user)
        {
            try
            {
                _db.Users.Remove(user);
                return true;
                //await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // TODO: add logging (ILogger)
                Console.WriteLine(e.Message);
                return false;
            }

        }
    }
}
