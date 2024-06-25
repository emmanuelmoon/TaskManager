using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using TaskManager.Interfaces;
using TaskManager.Models;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace TaskManager.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly IConfiguration _configuration;
  private readonly IUserRepository _userRepository;
  private readonly IUserService _userService;

  public AccountController(IUserRepository userRepository, IUserService userService, IConfiguration configuration)
  {
    _userRepository = userRepository;
    _userService = userService;
    _configuration = configuration;
  }

  [HttpGet("GetUserInfo"), Authorize()]
  public async Task<ActionResult<User>> GetUserInfo()
  {
    var email = User.FindFirst(ClaimTypes.Email).Value;
    var user = await _userRepository.GetUserByEmailAsync(email);
    return Ok(new { user.Id, user.Username, user.Email });
  }

  [HttpPost("register")]
  public async Task<ActionResult<User>> Register(SignUp signUpModel)
  {
    var UsernameExists = await _userService.UsernameExists(signUpModel.Username);
    if (UsernameExists)
    {
      return BadRequest("Username already exists");
    }
    var EmailExists = await _userService.EmailExists(signUpModel.Email);
    if (EmailExists)
    {
      return BadRequest("Email already exists");
    }
    var user = await _userService.RegisterUser(signUpModel);
    return Ok(new { user.Id, user.Username, user.Email });
  }

  [HttpPost("login")]
  public async Task<ActionResult<string>> Login(Login loginModel)
  {
    var user = await _userRepository.GetUserByEmailAsync(loginModel.Email);

    if (user == null)
    {
      return BadRequest("User Not Found");
    }

    if (!VerifyPasswordHash(loginModel.Password, user.PasswordHash, user.PasswordSalt))
    {
      return BadRequest("Wrong Password");
    }

    string token = CreateToken(user);
    var returnUser = new Classes.LoginReturn
    {
      Token = token,
      Id = user.Id,
      Username = user.Username,
      Email = user.Email
    };
    return Ok(returnUser);
  }

  private string CreateToken(User user)
  {
    List<Claim> claims = new List<Claim>
    {
      new Claim(ClaimTypes.Email, user.Email),
      new Claim(ClaimTypes.Role, user.Role)
    };

    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
        _configuration.GetSection("AppSettings:Token").Value));

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.Now.AddDays(1),
        signingCredentials: creds);

    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

    return jwt;
  }

  private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
  {
    using (var hmac = new HMACSHA512(passwordSalt))
    {
      var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      return computedHash.SequenceEqual(passwordHash);
    }
  }
}