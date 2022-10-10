using Application.Queries;
using Application.TodoAggregate;
using Infrastructure.Config;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.Repositories;
public class SearchTodoRepository : ISearchTodoRepository
{

     private readonly IMongoCollection<Todo> collection;
    private readonly ITodoDbContext context;
    private readonly ILogger<TodoRepository> logger;

    public SearchTodoRepository(ITodoDbContext context, ITodoDbConfig configuration, ILogger<TodoRepository> logger)
    {
        this.context = context;
        collection = context.GetCollection<Todo>(configuration.TodoCollectionName);
        this.logger = logger;
    }
    public Task<IEnumerable<SearchTodoResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SearchTodoResponse>> GetAllByCategory(ECategory category)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SearchTodoResponse>> GetAllByDate(DateOnly date)
    {
        throw new NotImplementedException();
    }

    public Task<SearchTodoResponse> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}