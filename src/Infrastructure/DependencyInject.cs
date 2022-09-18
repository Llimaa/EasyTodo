
using Application.TodoAggregate;
using Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInject
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {

        services.AddTransient<ITodoRepository, TodoRepository>();
        return services;
    }
}