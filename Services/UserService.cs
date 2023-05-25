using CoolAppInTheCloud.Data;
using CoolAppInTheCloud.Data.Models;
using CoolAppInTheCloud.Helpers_Extensions;
using Microsoft.EntityFrameworkCore;

namespace CoolAppInTheCloud.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByUsername(string username);
        Task<IEnumerable<User>> GetAllUsers();
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
    }

    public class UserService : IUserService
    {
        private readonly CoolAppInTheCloudDbContext _dbContext;

        public UserService(CoolAppInTheCloudDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task CreateUser(User user)
        {
            user.Password = user.Password.ToMd5();
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
