using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;

namespace TaskManager.Interfaces;

public interface IUserService
{
  void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
  Task<User> RegisterUser(SignUp signUpModel);
  Task<bool> UsernameExists(string username);
  Task<bool> EmailExists(string email);
}
