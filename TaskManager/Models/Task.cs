using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models;

public class Task
{
  public int Id { get; set; }
  [Required]
  public string Description { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime DueDate { get; set; }
  public string Status { get; set; }
  public int UserId { get; set; }
  public User User { get; set; }
}
