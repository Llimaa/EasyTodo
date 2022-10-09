using Application.TodoAggregate;
using Infrastructure;
using Infrastructure.Config;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class AddTodoDatabase 
{
    public static void AddTodo(this IServiceCollection services, IConfiguration config) 
    {
        services.Configure<ITodoDbConfig>(config.GetSection(nameof(TodoDbConfig)));
        services.AddTransient<ITodoDbConfig, TodoDbConfig>(_ => 
        _.GetRequiredService<IOptions<TodoDbConfig>>().Value);

        services.AddSingleton<ITodoDbContext, TodoDbContext>();
        services.AddSingleton<ITodoRepository, TodoRepository>();
    }
}