
using Application.TodoAggregate;
using Infrastructure;
using Infrastructure.Context;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInject
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IDbContext, DbContext>();
        services.AddTransient<ITodoRepository, TodoRepository>();
        return services;
    }
}