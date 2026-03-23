namespace StudentsManager.Mvc.Domain.Entities;

public class Event
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public DateTime DatetimeUtc { get; set; }

    public string? Type { get; set; }

    public string? Data { get; set; }
}