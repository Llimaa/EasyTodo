namespace Application.TodoAggregate;

public interface ITodoRepository
{
    Task<Guid?> Raise(Todo todo);
    Task Update(TodoUpdateRequest todo, Guid id);
    Task Remove(Guid id);
}