namespace Application.TodoAggregate;

public class TodoRequestUpdate 
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ECategory? Category { get; set; }
    public Status Status { get; set; }
}