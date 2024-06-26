﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
}
