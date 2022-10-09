using Infrastructure.Config;
using MongoDB.Driver;

namespace Infrastructure;

public interface ITodoDbContext 
{
    IMongoCollection<T> GetCollection<T>(string name);
    IClientSessionHandle? CurrentSession { get; }
    Task StartTransactionAsync();
    Task CommitTransactionAsync();
    Task AbortTransactionAsync();
}