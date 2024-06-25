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

  public ICollection<Models.Task> GetTasks()
  {
    return _context.Tasks.OrderBy(t => t.Id).ToList();
  }
}