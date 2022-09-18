namespace Application.TodoAggregate;

public class Todo
{
    public Todo(string? title, string? description, Type? type)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
        Title = title;
        Description = description;
        Type = type;
        Status = Status.Create;
    }

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public Type? Type { get; private set; }
    public Status Status { get; private set; }


    public void Update(string title, string description)
    {
        if (Status == Status.Create)
        {
            Title = title;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void UpdateType(Type type)
    {
        if (Status != Status.Finish)
            Type = type;
    }

    public void UpdateStatus()
    {
        if (Status != Status.Executed && Status != Status.Finish)
            Status = Status;
    }
}