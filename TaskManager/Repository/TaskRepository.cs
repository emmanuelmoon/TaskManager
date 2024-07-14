using TaskManager.Data;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly DataContext _context;
    public TaskRepository(DataContext context)
    {
        _context = context;
    }

    public Dictionary<string, int> GetTasksCounts()
    {
        return _context.Tasks.AsEnumerable()
            .GroupBy(task => task.Status)
            .ToDictionary(group => group.Key, group => group.Count());
    }
    public Dictionary<string, int> GetTasksCounts(string email)
    {
        return _context.Tasks.Where(x => x.User.Email == email).ToDictionary(x => x.Status).GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Count());
    }
    public ICollection<Models.Task> GetTasks(string filterText)
    {
        Console.WriteLine(string.IsNullOrEmpty(filterText));
        if (!string.IsNullOrEmpty(filterText))
        {
            return _context.Tasks.Where(x => x.Description.Contains(filterText)).ToList();
        }
        return _context.Tasks.ToList();
    }
    public ICollection<Models.Task> GetTasksByEmail(string email)
    {
        return _context.Tasks.Where(x => x.User.Email == email).ToList();
    }
    public Dictionary<string, object> GetTaskById(int id)
    {
        var task = _context.Tasks.Where(x => x.Id == id).FirstOrDefault();

        if (task == null)
        {
            return null;
        }

        var dictionary = new Dictionary<string, object>()
  {
      { "Id", task.Id },
      { "Description", task.Description },
      { "CreatedAt", task.CreatedAt },
      { "DueDate", task.DueDate },
      { "Status", task.Status },
      { "UserId", task.UserId },
  };

        if (task.User != null)
        {
            dictionary.Add("User", task.User);
        }

        return dictionary;
    }

    public Models.Task AddTask(NewTask task)
    {
        var newTask = new Models.Task
        {
            Description = task.Description,
            CreatedAt = DateTime.Now,
            DueDate = task.DueDate,
            Status = "Pending",
            UserId = 1,
        };
        _context.Tasks.Add(newTask);
        _context.SaveChanges();
        return newTask;
    }

    public Models.Task updateStatus(int id, string status)
    {
        var task = _context.Tasks.Where(x => x.Id == id).FirstOrDefault();
        if (task != null)
        {
            if (status == "InProgress")
            {
                task.Status = status;
                _context.SaveChanges();
                return task;
            }
            task.Status = status;
            _context.SaveChanges();
            return task;
        }

        return null;
    }
}
