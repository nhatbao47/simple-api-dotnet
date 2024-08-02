using Microsoft.EntityFrameworkCore;
using SimpleApi.Models;

public class SimpleApiContext : DbContext
{
    public SimpleApiContext(DbContextOptions<SimpleApiContext> options) : base(options)
    {
    }

    public DbSet<SimpleApi.Models.Task> Tasks { get; set; } = null;
    public DbSet<Schedule> Schedules { get; set; } = null;
    public DbSet<User> Users { get; set; } = null;
}