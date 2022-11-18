using Application.Queries;
using Application.TodoAggregate;
using Moq;

namespace EasyTodoTests.Queries;

public class SearchTodoTests 
{
    [Fact]
    public void WhenRequestGetByIdShouldReturnOneSearchTodoResponse()
    {
        var id = Guid.NewGuid();
        var resourceTodo = new SearchTodoResponse 
        {
            Id=  id,
            Title=  "Organizar rotinas",
            Description=  "Planejar rotinas da semana",
            Category= ECategory.Other,
            Status=  Status.Create
        };

        var mock = new Moq.Mock<ISearchTodoRepository>();
        mock.Setup(_ => _.GetById(id)).ReturnsAsync(resourceTodo);
        var todo =  mock.Object.GetById(id).Result;

        mock.Verify(_ => _.GetById(id), Times.Once);
        Assert.Equal(todo, resourceTodo);
    }

    [Fact]
    public void WhenRequestGetAllByCategoryShouldReturnOneSearchTodoResponse()
    {
        var resourceTodoList = GetListTodo();
        var mock = new Moq.Mock<ISearchTodoRepository>();
        mock.Setup(_ => _.GetAllByCategory(ECategory.Other)).ReturnsAsync(resourceTodoList);

        var firstRequest =  mock.Object.GetAllByCategory(ECategory.Other).Result;
        var secondRequest =  mock.Object.GetAllByCategory(ECategory.Other).Result;

        mock.Verify(_ => _.GetAllByCategory(ECategory.Other), Times.Exactly(2));
    }

    [Fact]
    public void WhenRequestGetAllShouldReturnOneSearchTodoResponse()
    {
        var resourceTodoList = GetListTodo();
        var mock = new Moq.Mock<ISearchTodoRepository>();
        mock.Setup(_ => _.GetAll()).ReturnsAsync(resourceTodoList);

        var todoList =  mock.Object.GetAll().Result;

        mock.Verify(_ => _.GetAll(), Times.Once);
    }

    private List<SearchTodoResponse> GetListTodo() 
    {
        var todoList = new List<SearchTodoResponse>  {
            new SearchTodoResponse()
            {
                Id=  Guid.NewGuid(),
                Title=  "Tarefa 01",
                Description=  "Ir para academia",
                Category= ECategory.Other,
                Status=  Status.Create
            },
            
            new SearchTodoResponse()
            {
                Id=  Guid.NewGuid(),
                Title=  "Tarefa 02",
                Description=  "Leitura de 30 minutos",
                Category= ECategory.Study,
                Status=  Status.Create
            }
        };
        return todoList;
    }
}