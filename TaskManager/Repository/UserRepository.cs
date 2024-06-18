using TaskManager.Data;
using TaskManager.Interfaces;
using TaskManager.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace TaskManager.Repository;

public class UserRepository : IUserRepository
{
  private readonly DataContext _context;
  public UserRepository(DataContext context)
  {
    _context = context;
  }

  public ICollection<User> GetUsers()
  {
    return _context.Users.OrderBy(p => p.Id).ToList();
  }
  public async Task<User> AddUser(User user)
  {
    // Validate user data (optional, can be done in service layer)
    // ... (validation logic if needed)

    // Add the user entity to the DbSet for Users in DbContext
    _context.Users.Add(user);

    // Save changes to the database
    await _context.SaveChangesAsync();

    // Return the newly created user (optional)
    return user;
  }
  public async Task<User> GetUserByUsernameAsync(string email)
  {
    // Explicitly specify the type for clarity and safety
    return await _context.Users.FirstOrDefaultAsync<User>(x => x.Email == email);
  }
}
