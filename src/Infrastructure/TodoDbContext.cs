using Infrastructure.Config;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Infrastructure;

public class TodoDbContext : ITodoDbContext
{
    private IMongoDatabase database { get; set; }
    private MongoClient mongoClient { get; set; }
    private IClientSessionHandle? session;

    [Obsolete]
    public TodoDbContext(ITodoDbConfig config)
    {
        SetUpConventions();
        mongoClient = new MongoClient(config.ConnectionString);
        database = mongoClient.GetDatabase(config.DatabaseName);
    }
    public IClientSessionHandle? CurrentSession => session;
    
    public IMongoCollection<T> GetCollection<T>(string name) => database.GetCollection<T>(name);

    [Obsolete]
    private void SetUpConventions()
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        MongoDefaults.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;
        
        var pack = new ConventionPack
        {
            new IgnoreExtraElementsConvention(true),
            new IgnoreIfDefaultConvention(true),
            new EnumRepresentationConvention(BsonType.String)
        };

        ConventionRegistry.Register("Conciliation Database Conventions", pack, t => true);
    }
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