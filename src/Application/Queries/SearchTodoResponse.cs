
using Application.TodoAggregate;

namespace Application.Queries;

public class SearchTodoResponse
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Type? Type { get; set; }
    public Status Status { get; set; }
}