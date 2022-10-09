namespace Infrastructure.Config;

public class TodoDbConfig : ITodoDbConfig
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string TodoCollectionName { get; set; } = null!;
}