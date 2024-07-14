using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Classes;
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
    public ActionResult<ICollection<Models.Task>> GetTasks(int page = 1, int pageSize = 10, string? filterText = "")
    {
        var user = User.FindFirst(ClaimTypes.Email);
        if (User.IsInRole("Admin"))
        {
            var totalTaskCount = _taskRepository.GetTasks(filterText).Count();
            var tasks = _taskRepository.GetTasks(filterText).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Ok(PagedList<Models.Task>.Create(tasks, page, pageSize, totalTaskCount));
        }
        var userTaskCount = _taskRepository.GetTasksByEmail(user.Value).Count();
        var userTasks = _taskRepository.GetTasksByEmail(user.Value).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return Ok(PagedList<Models.Task>.Create(userTasks, page, pageSize, userTaskCount));
    }

    [HttpPost("add-task")]
    [Authorize]
    public ActionResult<Models.Task> AddTask(NewTask task)
    {
        var user = User.FindFirst(ClaimTypes.Email);
        var newTask = _taskRepository.AddTask(task);
        return Ok(newTask);
    }

    [HttpPut("changeStatus/{id}/{status}")]
    [Authorize]
    public ActionResult<Models.Task> changeStatus(int id, string status)
    {
        var response = _taskRepository.updateStatus(id, status);
        if (response == null)
        {
            return StatusCode(404, "Not a valid request.");
        }
        return Ok(response);
    }
}
