using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
  public class SignUpModel
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
  }
}