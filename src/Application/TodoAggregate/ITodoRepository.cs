namespace Application.TodoAggregate;

public interface ITodoRepository
{
    Task Raise(Todo todo);
    Task Update(Todo todo);
    Task<Todo> GetById(Guid id);
    Task<List<Todo>> GetAll();
    Task<List<Todo>> GetAllByMonth(DateOnly date);
    Task Remove(Guid id);
}