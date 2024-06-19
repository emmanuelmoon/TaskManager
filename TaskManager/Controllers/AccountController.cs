using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using TaskManager.Interfaces;
using TaskManager.Models;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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

  [HttpGet(Name = "GetUserName")]
  [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
  public IActionResult GetUsers()
  {
    var users = _userRepository.GetUsers();

    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    return Ok(users);
  }
  [HttpPost("register")]
  public async Task<ActionResult<User>> Register(SignUp signUpModel)
  {
    var user = await _userService.RegisterUser(signUpModel);
    return Ok(new { user.Id, user.Username, user.Email });
  }

  [HttpPost("login")]
  public async Task<ActionResult<string>> Login(Login loginModel)
  {
    var user = await _userRepository.GetUserByUsernameAsync(loginModel.Email);

    if (user == null)
    {
      return BadRequest("User Not Found");
    }

    if (!VerifyPasswordHash(loginModel.Password, user.PasswordHash, user.PasswordSalt))
    {
      return BadRequest("Wrong Password");
    }

    string token = CreateToken(user);

    return Ok(token);
  }

  private string CreateToken(User user)
  {
    List<Claim> claims = new List<Claim>
    {
      new Claim(ClaimTypes.Email, user.Email),
      // new Claim(ClaimTypes.Role, "Admin")
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