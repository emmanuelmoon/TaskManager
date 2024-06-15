using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly ILogger<AccountController> _logger;

  public AccountController(ILogger<AccountController> logger)
  {
    _logger = logger;
  }

  [HttpGet(Name = "GetUserName")]
  public string GetUserName()
  {
    return "John Doe";
  }
}