using Infrastructure.Config;
using MongoDB.Driver;

namespace Infrastructure;

public class TodoDbContext : ITodoDbContext
{
    private IMongoDatabase database { get; set; }
    private MongoClient mongoClient { get; set; }
    private IClientSessionHandle? session;

    public TodoDbContext(ITodoDbConfig config)
    {
        mongoClient = new MongoClient(config.ConnectionString);
        database = mongoClient.GetDatabase(config.DatabaseName);
    }
    public IClientSessionHandle? CurrentSession => session;
    
    public IMongoCollection<T> GetCollection<T>(string name) => database.GetCollection<T>(name);
    
    public async Task AbortTransactionAsync()
    {
        if(session is not null && session.IsInTransaction)
            await session.CommitTransactionAsync().ConfigureAwait(false);

        if(session is not null)
            session.Dispose();
    }

    public async Task CommitTransactionAsync()
    {
        if(session is not null && session.IsInTransaction)
            await session.CommitTransactionAsync().ConfigureAwait(false);
    }

    public async Task StartTransactionAsync()
    {
        session = await mongoClient.StartSessionAsync().ConfigureAwait(false);
        session.StartTransaction();
    }
}