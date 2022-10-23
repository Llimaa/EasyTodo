using Application.TodoAggregate;

namespace Application.Queries;

public interface ISearchTodoRepository 
{
    Task<SearchTodoResponse> GetById(Guid id);
    Task<IEnumerable<SearchTodoResponse>> GetAll();
    Task<IEnumerable<SearchTodoResponse>> GetAllByCategory(ECategory category);
}