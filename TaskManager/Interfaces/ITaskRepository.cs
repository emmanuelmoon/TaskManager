// using TaskManager.Models;

namespace TaskManager.Interfaces
{
  public interface ITaskRepository
  {
    ICollection<Models.Task> GetTasks();
  }
}
