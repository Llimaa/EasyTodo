using Application.TodoAggregate;

namespace Application.Queries;

public interface ISearchTodoRepository 
{
    Task<SearchTodoResponse> GetById(Guid id);
    Task<IEnumerable<SearchTodoResponse>> GetAll();
    Task<IEnumerable<SearchTodoResponse>> GetAllByDate(DateTime date);
    Task<IEnumerable<SearchTodoResponse>> GetAllByCategory(ECategory category);
}