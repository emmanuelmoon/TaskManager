using System.Security.Cryptography;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManagerServices;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;
  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }
  public void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
  {
    using (var hmac = new HMACSHA512())
    {
      passwordSalt = hmac.Key;
      passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
  }

  public async Task<User> RegisterUser(SignUp signUpModel)
  {
    byte[] passwordHash, passwordSalt;
    CreatePassword(signUpModel.Password, out passwordHash, out passwordSalt);
    var user = new User
    {
      Username = signUpModel.Username,
      Email = signUpModel.Email,
      PasswordHash = passwordHash,
      PasswordSalt = passwordSalt,
      Role = "User"
    };
    var result = await _userRepository.AddUser(user);
    return result;
  }

  public async Task<bool> UsernameExists(string username)
  {
    var user = await _userRepository.GetUserByUsernameAsync(username);
    return user != null;
  }
  public async Task<bool> EmailExists(string username)
  {
    var user = await _userRepository.GetUserByEmailAsync(username);
    return user != null;
  }

}
