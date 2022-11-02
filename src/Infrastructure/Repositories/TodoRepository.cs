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

    public async Task<Guid?>  Raise(Todo todo)
    {
        try
        {
            if(context.CurrentSession is null)
            {
                logger.LogError("Todo can't be insert without session scope");
                return null;
            }

            await collection.InsertOneAsync(todo).ConfigureAwait(false);
            return todo.Id;
        }
        catch (Exception exception)
        {
            logger.LogError("Error trying to insert one todo, message: {message} at:{now} ", exception.Message, DateTime.UtcNow);
            return null;
        }
    }

    public async Task Remove(Guid id)
    {
        try
        {
            if(context.CurrentSession is null)
                logger.LogError("Todo can't be remove without session scope");

            await collection.DeleteOneAsync(_ => _.Id == id).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            logger.LogError("Error trying to remove one todo, message: {message} at:{now} ", exception.Message, DateTime.UtcNow);
        }
    }

    public async Task Update(TodoUpdateRequest todo, Guid id)
    {
        if(context.CurrentSession is null)
            logger.LogError("Todo can't be update without session scope");

        var builderFilter = Builders<Todo>.Filter;
        var updateBuilder = Builders<Todo>.Update;
        var filter = builderFilter.Eq(_ => _.Id, id);
        var update = updateBuilder
            .Set(definition => definition.Title, todo.Title)
            .Set(definition => definition.Description, todo.Description)
            .Set(definition => definition.Category, todo.Category)
            .Set(definition => definition.Status, todo.Status);
        
        var updateDocument = new UpdateOneModel<Todo>(filter, update) { IsUpsert = true } ;
        
        try
        {
            await collection.UpdateOneAsync(filter, update).ConfigureAwait(false);
        } 
        catch(Exception exception) 
        {
            logger.LogError("Error Trying to update one todo, message: {message} at: {at} ", exception.Message, DateTime.UtcNow);
        }
    }
}