using Application.TodoAggregate;
using Dapper;
using Infrastructure.Context;

namespace Infrastructure;

public class TodoRepository : ITodoRepository
{

    private readonly IDbContext dbContext;

    public TodoRepository(IDbContext dbContext) => this.dbContext = dbContext;

    public async Task<IEnumerable<Todo>> GetAll()
    {
        var query = "SELECT * FROM Todo";

        using var dbConnection = dbContext.GetCon();
        dbConnection.Open();

        var response = await dbConnection.QueryAsync<Todo>(query);
        return response;
    }

    public async Task<IEnumerable<Todo>> GetAllByMonth(DateOnly date)
    {
        var query = "SELECT * FROM  Todo WHERE CreatedAt BETWEEN @Date AND @LastDate";

        using var dbConnection = dbContext.GetCon();
        dbConnection.Open();

        var response = await dbConnection.QueryAsync<Todo>(query, new { Date = date, LastDate = date.AddDays(30) });

        return response;
    }

    public async Task<IEnumerable<Todo>> GetAllByType(ECategory category)
    {
        var query = "SELECT * FROM Todo WHERE Category = @Category";
        using var dbConnection = dbContext.GetCon();
        dbConnection.Open();

        var response = await dbConnection.QueryAsync<Todo>(query, new { Category = category });
        return response;
    }



    public async Task<Todo> GetById(Guid id)
    {
        var query = "SELECT * FROM Todo WHERE Id = @Id";

        using var dbConnection = dbContext.GetCon();
        dbConnection.Open();

        var response = await dbConnection.QueryFirstOrDefaultAsync<Todo>(query, new { Id = id });
        return response;
    }

    public async Task Raise(Todo todo)
    {
        var query = "INSERT INTO Todo (Id, CreatedAt, Title, Description, Type, Status) VALUES(@Id, @CreatedAt, @Title, @Description, @Type, @Status)";

        using var dbConnection = dbContext.GetCon();
        dbConnection.Open();
        await dbConnection.ExecuteAsync(query, new
        {
            @Id = todo.Id,
            @CreatedAt = todo.CreatedAt,
            @Title = todo.Title,
            @Description = todo.Description,
            @Type = todo.Type
        });
    }

    public async Task Remove(Guid id)
    {
        var query = "DELETE FROM Todo WHERE Id = @Id";
        using var dbConnection = dbContext.GetCon();
        dbConnection.Open();

        await dbConnection.ExecuteAsync(query, new { Id = id });
        await Task.CompletedTask;
    }

    public async Task Update(Todo todo)
    {
        var query = "UPDATE Todo SET UpdatedAt = @UpdatedAt, Title = @Title, Description = @Description, Type = @Type, Status = @ Status) WHERE Id = @Id";
        using var dbConnection = dbContext.GetCon();
        dbConnection.Open();

        await dbConnection.ExecuteAsync(query, new { Id = todo.Id, UpdateAt = todo.UpdatedAt, Title = todo.Title, Description = todo.Description, Type = todo.Type, Status = todo.Status });
        await Task.CompletedTask;
    }
}