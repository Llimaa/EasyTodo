namespace Application.TodoAggregate;

public class Todo
{
    public Todo(string? title, string? description, ECategory? category)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
        Title = title;
        Description = description;
        Category = category;
        Status = Status.Create;
    }

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public ECategory? Category { get; private set; }
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

    public void UpdateCategory(ECategory category)
    {
        if (Status != Status.Finish) {
            UpdatedAt = DateTime.UtcNow;
            Category = category;
        }
    }

    public void UpdateStatus()
    {
        if (Status != Status.Executed && Status != Status.Finish) {
            UpdatedAt = DateTime.UtcNow;
            Status = Status;
        }
    }

    public static Todo BindingToTodo(TodoRequestRaise todoRequest) 
    {
        var instantiate = new Todo(todoRequest.Title, todoRequest.Description, todoRequest.Category);
        return instantiate;
    }
}