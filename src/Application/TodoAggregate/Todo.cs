using Application.TodoAggregate.Request;
namespace Application.TodoAggregate;

public class Todo
{

    private Todo(string? title, string? description, ECategory? category)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
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

    public static Todo BindingToTodo(TodoRaiseRequest todoRequest) 
    {
        var instantiate = new Todo(todoRequest.Title, todoRequest.Description, todoRequest.Category);
        return instantiate;
    }
}