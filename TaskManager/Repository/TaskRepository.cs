using TaskManager.Data;
using TaskManager.Interfaces;

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
    return _context.Tasks.ToDictionary(x => x.Status).GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Count());
  }
  public Dictionary<string, int> GetTasksCounts(string email)
  {
    return _context.Tasks.Where(x => x.User.Email == email).ToDictionary(x => x.Status).GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Count());
  }
  public ICollection<Models.Task> GetTasks()
  {
    return _context.Tasks.ToList();
  }
  public ICollection<Models.Task> GetTasks(string email)
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
}