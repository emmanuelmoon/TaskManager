using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
    public void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using (var hmac = new HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      byte[] passwordHash, passwordSalt;
      CreatePassword("admin1234", out passwordHash, out passwordSalt);
      modelBuilder.Entity<User>().HasData(
        new User { Id = 1, Username = "admin", Email = "admin@gmail.com", PasswordHash = passwordHash, PasswordSalt = passwordSalt, Role = "Admin" }
      );
      modelBuilder.Entity<User>()
        .HasMany(u => u.Tasks)
        .WithOne(t => t.User)
        .HasForeignKey(t => t.UserId);


      modelBuilder.Entity<Models.Task>().HasData(
            new Models.Task
            {
              Id = 1,
              Description = "Setup Admin Dashboard",
              CreatedAt = DateTime.Now.AddDays(-7),
              DueDate = DateTime.Now.AddDays(-1),
              Status = "Completed",
              UserId = 1
            },
            new Models.Task
            {
              Id = 2,
              Description = "Create Initial Users",
              CreatedAt = DateTime.Now.AddDays(-2),
              DueDate = DateTime.Now.AddDays(3),
              Status = "InProgress",
              UserId = 1
            },
            new Models.Task
            {
              Id = 3,
              Description = "Review System Logs",
              Status = "Pending",
              CreatedAt = DateTime.Now.AddDays(2),
              DueDate = DateTime.Now.AddDays(7),
              UserId = 1
            }
        );
    }
  }
}

