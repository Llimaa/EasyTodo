using Application.TodoAggregate;
using Infrastructure.Config;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly IMongoCollection<Todo> collection;
    private readonly ITodoDbContext context;
    private readonly ILogger<TodoRepository> logger;

    public TodoRepository(ITodoDbContext context, ITodoDbConfig configuration, ILogger<TodoRepository> logger)
    {
        this.context = context;
        collection = context.GetCollection<Todo>(configuration.TodoCollectionName);
        this.logger = logger;
    }

    public Task<Guid>  Raise(Todo todo)
    {
        throw new NotImplementedException();
    }

    public Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Update(TodoRequestUpdate todo)
    {
        throw new NotImplementedException();
    }
}
