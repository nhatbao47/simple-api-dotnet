namespace SimpleApi.DTOs;

public class ScheduleDto
{
    public int Id { get; set;}
    public string? Title { get; set;}
    public string? Description { get; set;}
    public string? Location  { get; set;}
    public DateTime Date { get; set;}
    public string? StartTime { get; set;}
    public string? EndTime { get; set;}
    public int UserId { get; set;}
    public string? Creator {get;set;}
}