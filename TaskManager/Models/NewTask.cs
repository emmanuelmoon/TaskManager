using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models;

public class NewTask
{
    [Required]
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
}
