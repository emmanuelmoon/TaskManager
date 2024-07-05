// using TaskManager.Models;

namespace TaskManager.Interfaces
{
  public interface ITaskRepository
  {
    Dictionary<string, int> GetTasksCounts();
    Dictionary<string, int> GetTasksCounts(string email);
    ICollection<Models.Task> GetTasks();
    ICollection<Models.Task> GetTasks(string email);
    Dictionary<string, object> GetTaskById(int id);
    Models.Task AddTask(Models.NewTask task);
  }
}
