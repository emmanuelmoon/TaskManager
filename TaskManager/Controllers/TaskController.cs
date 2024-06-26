using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Interfaces;

namespace TaskManager.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
  private readonly ITaskRepository _taskRepository;
  private readonly IConfiguration _configuration;
  public TaskController(ITaskRepository taskRepository, IConfiguration configuration)
  {
    _taskRepository = taskRepository;
    // _userService = userService;
    _configuration = configuration;
  }

  [HttpGet("count")]
  [Authorize]
  public ActionResult<int> GetTaskCount()
  {
    var user = User.FindFirst(ClaimTypes.Email);
    if (User.IsInRole("Admin"))
    {
      Dictionary<string, int> allTasks = _taskRepository.GetTasksCounts();
      return Ok(allTasks);
    }

    return Ok();
  }
}
