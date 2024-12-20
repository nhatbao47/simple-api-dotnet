namespace SimpleApi.Models;

public class User
{
    public int Id { get; set;}
    public string? UserName { get; set;}
    public string? Password { get; set;}
    public string? FullName { get; set;}
    public string? Title { get; set;}
    public ICollection<Schedule>? Schedules { get; set; }
}