using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Interfaces;
using TaskManager.Models;

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
  public ActionResult<Dictionary<string, int>> GetTaskCount()
  {
    var user = User.FindFirst(ClaimTypes.Email);
    if (User.IsInRole("Admin"))
    {
      Dictionary<string, int> allTasks = _taskRepository.GetTasksCounts();
      return Ok(allTasks);
    }
    Dictionary<string, int> tasks = _taskRepository.GetTasksCounts(user.Value);
    return Ok(tasks);
  }

  [HttpGet("task/{id}")]
  [Authorize]
  public ActionResult<Dictionary<string, int>> GetTaskDetail(int id)
  {
    Dictionary<string, object> taskDetail = _taskRepository.GetTaskById(id);
    return Ok(taskDetail);
  }

  [HttpGet("tasks")]
  [Authorize]
  public ActionResult<ICollection<Models.Task>> GetTasks()
  {
    var user = User.FindFirst(ClaimTypes.Email);
    if (User.IsInRole("Admin"))
    {
      return Ok(_taskRepository.GetTasks());
    }
    return Ok(_taskRepository.GetTasks(user.Value));
  }

  [HttpPost("add-task")]
  [Authorize]
  public ActionResult<Models.Task> AddTask(NewTask task)
  {
    var user = User.FindFirst(ClaimTypes.Email);
    var newTask = _taskRepository.AddTask(task);
    return Ok(newTask);
  }

}
