using Application.Queries;
using Application.TodoAggregate;
using Infrastructure.Config;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
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
    public async Task<IEnumerable<SearchTodoResponse>> GetAll()
    {
        if(context.CurrentSession is null) 
        {
            logger.LogError("Todo can't be inserted without session scope");
            return null;
        }

        var items = await collection 
            .Aggregate()
            .Project<SearchTodoResponse>(new BsonDocument {
                { nameof(SearchTodoResponse.Id), "$_id"},
                { nameof(SearchTodoResponse.Title), $"${nameof(Todo.Title)}" },
                { nameof(SearchTodoResponse.Description), $"${nameof(Todo.Description)}" },
                { nameof(SearchTodoResponse.Category), $"${nameof(Todo.Category)}" },
                { nameof(SearchTodoResponse.Status), $"${nameof(Todo.Status)}" }
            }).ToListAsync();
        return items;
;    }

    public async Task<IEnumerable<SearchTodoResponse>> GetAllByCategory(ECategory category)
    {
        var builderFilter = Builders<Todo>.Filter;
        var filter = builderFilter.Eq(_ => _.Category, category);

        var items = await collection 
            .Aggregate()
            .Match(filter)
            .Project<SearchTodoResponse>(new BsonDocument {
                { nameof(SearchTodoResponse.Id), "$_id"},
                { nameof(SearchTodoResponse.Title), $"${nameof(Todo.Title)}" },
                { nameof(SearchTodoResponse.Description), $"${nameof(Todo.Description)}" },
                { nameof(SearchTodoResponse.Category), $"${nameof(Todo.Category)}" },
                { nameof(SearchTodoResponse.Status), $"${nameof(Todo.Status)}" }
            }).ToListAsync();

        return items;
    }

    public async Task<SearchTodoResponse> GetById(Guid id)
    {
        var builderFilter = Builders<Todo>.Filter;
        var filter = builderFilter.Eq(_ => _.Id, id);

        var item = await collection 
            .Aggregate()
            .Match(filter)
            .Project<SearchTodoResponse>(new BsonDocument {
                { nameof(SearchTodoResponse.Id), "$_id"},
                { nameof(SearchTodoResponse.Title), $"${nameof(Todo.Title)}" },
                { nameof(SearchTodoResponse.Description), $"${nameof(Todo.Description)}" },
                { nameof(SearchTodoResponse.Category), $"${nameof(Todo.Category)}" },
                { nameof(SearchTodoResponse.Status), $"${nameof(Todo.Status)}" }
            }).FirstOrDefaultAsync();

        return item;
    }
}