using Microsoft.EntityFrameworkCore;
using SimpleApi.Models;
using Task = SimpleApi.Models.Task;

namespace SimpleApi;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new SimpleApiContext(
            serviceProvider.GetRequiredService<DbContextOptions<SimpleApiContext>>()))
        {
            // Check if the database is already seeded
            if (context.Tasks.Any())
            {
                return;   // DB has been seeded
            }

            // Add test data
            context.Tasks.AddRange(
                new Task { Title = "Task 1", Description = "Task description 1", State = TaskState.New },
                new Task { Title = "Task 2", Description = "Task description 2", State = TaskState.New },
                new Task { Title = "Task 3", Description = "Task description 3", State = TaskState.InProgress },
                new Task { Title = "Task 4", Description = "Task description 4", State = TaskState.InProgress },
                new Task { Title = "Task 5", Description = "Task description 5", State = TaskState.Done },
                new Task { Title = "Task 6", Description = "Task description 6", State = TaskState.Done }
            );

            context.Users.AddRange(
                new User {UserName = "admin", FullName = "Administrator", Title = "Boss", Password = "Ghsdy213"},
                new User {UserName = "user1", FullName = "David Nguyen", Title = "Developer"},
                new User {UserName = "user2", FullName = "Peter Pham", Title = "QC"},
                new User {UserName = "user3", FullName = "Lina Ngo", Title = "QA"}
            );

            context.Schedules.AddRange(
                new Schedule {Title = "Meeting 1", Description = "Test description 1", Location = "HCM", Date = DateTime.Today, StartTime = "9:00", EndTime = "10:00", UserId = 2},
                new Schedule {Title = "Meeting 2", Description = "Test description 2", Location = "HN", Date = DateTime.Today.AddDays(1), StartTime = "11:00", EndTime = "12:00", UserId = 3},
                new Schedule {Title = "Meeting 3", Description = "Test description 3", Location = "DN", Date = DateTime.Today.AddDays(2), StartTime = "14:00", EndTime = "15:00", UserId = 4}
            );

            context.SaveChanges();
        }
    }
}
