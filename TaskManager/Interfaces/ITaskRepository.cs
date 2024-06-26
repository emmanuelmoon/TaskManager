// using TaskManager.Models;

namespace TaskManager.Interfaces
{
  public interface ITaskRepository
  {
    Dictionary<string, int> GetTasksCounts();
  }
}
