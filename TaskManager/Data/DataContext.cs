using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
  public class DataContext: DbContext
  {
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().HasData(
        new User { Id = 1, FullName = "Admin", Username = "admin", Email = "johndoe@gmail.com", Password="123" }
      );
    }
  }
}

