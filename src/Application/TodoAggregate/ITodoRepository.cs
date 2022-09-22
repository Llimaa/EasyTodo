namespace Application.TodoAggregate;

public interface ITodoRepository
{
    Task Raise(Todo todo);
    Task Update(Todo todo);
    Task<Todo> GetById(Guid id);
    Task<IEnumerable<Todo>> GetAll();
    Task<IEnumerable<Todo>> GetAllByMonth(DateOnly date);
    Task<IEnumerable<Todo>> GetAllByType(ECategory category);
    Task Remove(Guid id);
}