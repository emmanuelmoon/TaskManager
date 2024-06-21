using TaskManager.Models;

namespace TaskManager.Interfaces;

public interface IUserRepository
{
  Task<User> AddUser(User user);
  ICollection<User> GetUsers();
  Task<User> GetUserByUsernameAsync(string email);
  Task<User> GetUserByEmailAsync(string email);
}
