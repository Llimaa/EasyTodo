
using Application.TodoAggregate;

namespace Application.Queries;

public class SearchTodoResponse
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ECategory? Category { get; set; }
    public Status Status { get; set; }
}