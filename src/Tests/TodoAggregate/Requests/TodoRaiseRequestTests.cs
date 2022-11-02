using Application.ErrorBag;
using Application.TodoAggregate;
using Application.TodoAggregate.Request;
using EnumsNET;
using FluentAssertions;
using FluentValidation;

namespace EasyTodoTests.TodoAggregate.Requests;

public class TodoRaiseRequestTests 
{
    private readonly IValidator<TodoRaiseRequest> validator;
    private readonly IErrorBagHandler errorBag;

public TodoRaiseRequestTests()
{
    this.validator= new TodoRaiseValidator();
    this.errorBag = new ErrorBagHandler();
}

    [Fact]
    public void WhenTodoRaiseRequestIsValidYourValidShouldBeTrue()
    {   
        // Given
        var title = "Task of this day";
        var description = "Read ten page of the book Clean Code";
        var category = ECategory.Study;

        var todoRaise = new TodoRaiseRequest(title, description, category);
    
        // When
        todoRaise.Validate(validator,errorBag);
    
        // Then
        Assert.True(todoRaise.Valid);
    }

    [Fact]
    public void WhenTodoRaiseRequestIsNotValidYourValidShouldBeFalse()
    {   
        // Given
        var title = "Task of this day";
        var description = "Read ten page of the book Clean Code";

        var todoRaise = new TodoRaiseRequest(title, description, null);
    
        // When
        todoRaise.Validate(validator,errorBag);
    
        // Then
        Assert.False(todoRaise.Valid);
    }

    [Fact]
    public void WhenTodoRaiseRequestIsNotValidYourErrorsShouldBeMoreThanZero()
    {   
        // Given
        var title = "Task of this day";
        var description = "";

        var todoRaise = new TodoRaiseRequest(title, description, null);
    
        // When
        todoRaise.Validate(validator,errorBag);
    
        // Then
        todoRaise.Errors.Should().HaveCountGreaterThan(1);
    }

    [Fact]
    public void WhenTodoRaiseRequestIsCreatedYourTitleIsEmptyShouldBeErrorWithMessage()
    {   
        // Given
        var title = "";
        var description = "Read ten page of the book Clean Code";
        var category = ECategory.Study;

        var todoRaise = new TodoRaiseRequest(title, description, category);
    
        // When
        todoRaise.Validate(validator,errorBag);
    
        // Then
        var isEquals =todoRaise?.Errors?.FirstOrDefault().Value == TodoRaiseValidatorError.Todo_Title_Is_Required.AsString(EnumFormat.Description);
        Assert.True(isEquals);
    }

    [Fact]
    public void WhenTodoRaiseRequestIsCreatedYourDescriptionIsNullShouldBeErrorWithMessage()
    {   
        // Given
        var title = "Task of this day";
        var category = ECategory.Study;

        var todoRaise = new TodoRaiseRequest(title, null, category);
    
        // When
        todoRaise.Validate(validator,errorBag);
    
        // Then
        var isEquals =todoRaise?.Errors?.FirstOrDefault().Value == TodoRaiseValidatorError.Todo_Description_Is_Required.AsString(EnumFormat.Description);
        Assert.True(isEquals);
    }

    [Fact]
    public void WhenTodoRaiseRequestIsCreatedYourCategoryIsNullShouldBeErrorWithMessage()
    {   
        // Given
        var title = "Task of this day";
        var description = "Read ten page of the book Clean Code";

        var todoRaise = new TodoRaiseRequest(title, description, null);
    
        // When
        todoRaise.Validate(validator,errorBag);
    
        // Then
        Assert.Equal(todoRaise?.Errors?.FirstOrDefault().Value, TodoRaiseValidatorError.Todo_Category_Is_Required.AsString(EnumFormat.Description));
    }
}