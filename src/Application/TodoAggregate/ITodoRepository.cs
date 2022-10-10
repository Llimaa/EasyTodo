namespace Application.TodoAggregate;

public interface ITodoRepository
{
    Task<Guid> Raise(Todo todo);
    Task Update(TodoRequestUpdate todo);
    Task Remove(Guid id);
}