using Application.TodoAggregate;
using Application.TodoAggregate.Request;

namespace EasyTodoTests.TodoAggregate;

public class TodoTests 
{
    [Fact]
    public void WhenTodoIsCreatedYourStatusMustBeCreate()
    {
        // Given
        var title = "Task of this day";
        var description = "Read ten page of the book Clean Code";
        var category = ECategory.Study;

        var todoRaiseRequest = new TodoRaiseRequest(title, description, category);
    
        // When
        var todo= Todo.BindingToTodo(todoRaiseRequest);
    
        // Then
        Assert.Equal(todo.Status, Status.Create);
    }

    [Fact]
    public void WhenTodoIsCreatedYourUpdatedAtMustBeNull() 
    {
        // Given
        var title = "Task of this day";
        var description = "Read ten page of the book Clean Code";
        var category = ECategory.Work;

        var todoRaiseRequest = new TodoRaiseRequest(title, description, category);

        // When
        var todo= Todo.BindingToTodo(todoRaiseRequest);

        // Then
        Assert.Equal(todo.Status, Status.Create);
    }

    [Fact]
    public void WhenTodoIsCreatedYourCreatedAtMustBeValid() 
    {
        // Given
        var title = "Task of this day";
        var description = "Read ten page of the book Clean Code";
        var category = ECategory.Study;

        var todoRaiseRequest = new TodoRaiseRequest(title, description, category);

        // When
        var todo= Todo.BindingToTodo(todoRaiseRequest);
        var isValid = DateTime.TryParse(todo.CreatedAt.ToShortDateString(), out _);
        
        // Then
        Assert.True(isValid);
    }

        [Fact]
    public void WhenTodoIsCreatedWithCategoryEqualWorkYourReturnShouldBeCategoryEqualWork() 
    {
        // Given
        var title = "Task of this day";
        var description = "Read ten page of the book Clean Code";
        var category = ECategory.Work;

        var todoRaiseRequest = new TodoRaiseRequest(title, description, category);

        // When
        var todo= Todo.BindingToTodo(todoRaiseRequest);
        
        // Then
        Assert.Equal(todo.Category, ECategory.Work);
    }

    [Fact]
    public void WhenTodoIsCreatedYourIdShouldBeNotEmpty() 
    {
        // Given
        var title = "Task of this day";
        var description = "Read ten page of the book Clean Code";
        var category = ECategory.Study;

        var todoRaiseRequest = new TodoRaiseRequest(title, description, category);

        // When
        var todo= Todo.BindingToTodo(todoRaiseRequest);
        
        // Then
        Assert.NotEqual(todo.Id, Guid.Empty);
    }

    [Fact]
    public void WhenTodoIsCreatedYourCreatedAtShouldBeToday() 
    {
        // Given
        var title = "Task of this day";
        var description = "Read ten page of the book Clean Code";
        var category = ECategory.Study;

        var todoRaiseRequest = new TodoRaiseRequest(title, description, category);

        // When
        var todo= Todo.BindingToTodo(todoRaiseRequest);
        
        // Then
        Assert.Equal(DateTime.Today.ToString("MM/dd/yyyy"), todo.CreatedAt.ToString("MM/dd/yyyy"));
    }
}