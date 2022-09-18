using Application.TodoAggregate;

namespace Infrastructure;

public class TodoRepository : ITodoRepository
{
    public Task<List<Todo>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<Todo>> GetAllByMonth(DateOnly date)
    {
        throw new NotImplementedException();
    }

    public Task<Todo> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Raise(Todo todo)
    {
        throw new NotImplementedException();
    }

    public Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Todo todo)
    {
        throw new NotImplementedException();
    }
}