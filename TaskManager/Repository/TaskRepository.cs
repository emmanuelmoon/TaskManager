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
}