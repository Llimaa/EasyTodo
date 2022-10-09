namespace Infrastructure.Config;

public interface ITodoDbConfig 
{
    string ConnectionString { get; }
    string DatabaseName { get; }
    string TodoCollectionName { get; }
}
