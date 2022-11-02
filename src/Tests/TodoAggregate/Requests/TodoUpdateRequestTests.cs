using Application.ErrorBag;
using Application.TodoAggregate;
using Application.TodoAggregate.Request;
using EnumsNET;
using FluentAssertions;
using FluentValidation;

namespace EasyTodoTests.TodoAggregate.Requests;

public class TodoUpdateRequestTests 
{
    private readonly IValidator<TodoUpdateRequest> validator;
    private readonly IErrorBagHandler errorBag;

    public TodoUpdateRequestTests()
    {
        this.validator= new TodoUpdateValidator();
        this.errorBag = new ErrorBagHandler();
    }

    [Fact]
    public void WhenTodoRaiseRequestIsValidYourValidShouldBeTrue()
    {   
        // Given
        var title = "Task of this day";
        var description = "Read ten page of the book Clean Code";
        var category = ECategory.Study;
        var status = Status.Executing;

        var todoRaise = new TodoUpdateRequest(title, description, category, status);
    
        // When
        todoRaise.Validate(validator,errorBag);
    
        // Then
        Assert.True(todoRaise.Valid);
    }

    [Fact]
    public void WhenTodoUpdateRequestIsNotValidYourValidShouldBeFalse()
    {   
        // Given
        var title = "Task of this day";
        var description = "Read ten page of the book Clean Code";
        var status = Status.Executing;

        var todoRaise = new TodoUpdateRequest(title, description, null, status);
    
        // When
        todoRaise.Validate(validator,errorBag);
    
        // Then
        Assert.False(todoRaise.Valid);
    }

    [Fact]
    public void WhenTodoUpdateRequestIsNotValidYourErrorsShouldBeMoreThanZero()
    {   
        // Given
        var title = "Task of this day";
        var description = "";
        var status = Status.Executing;

        var todoRaise = new TodoUpdateRequest(title, description, null, status);
    
        // When
        todoRaise.Validate(validator,errorBag);
    
        // Then
        todoRaise.Errors.Should().HaveCountGreaterThan(1);
    }

    [Fact]
    public void WhenTodoUpdateRequestIsCreatedYourTitleIsEmptyShouldBeErrorWithMessage()
    {   
        // Given
        var title = "";
        var description = "Read ten page of the book Clean Code";
        var category = ECategory.Study;
        var status = Status.Executing;

        var todoRaise = new TodoUpdateRequest(title, description, category, status);
    
        // When
        todoRaise.Validate(validator,errorBag);
    
        // Then
        var isEquals =todoRaise?.Errors?.FirstOrDefault().Value == TodoUpdateValidatorError.Todo_Title_Is_Required.AsString(EnumFormat.Description);
        Assert.True(isEquals);
    }

    [Fact]
    public void WhenTodoUpdateRequestIsCreatedYourDescriptionIsNullShouldBeErrorWithMessage()
    {   
        // Given
        var title = "Task of this day";
        var category = ECategory.Study;
        var status = Status.Executing;

        var todoRaise = new TodoUpdateRequest(title, null, category, status);
    
        // When
        todoRaise.Validate(validator,errorBag);
    
        // Then
        var isEquals =todoRaise?.Errors?.FirstOrDefault().Value == TodoUpdateValidatorError.Todo_Description_Is_Required.AsString(EnumFormat.Description);
        Assert.True(isEquals);
    }

    [Fact]
    public void WhenTodoUpdateRequestIsCreatedYourCategoryIsNullShouldBeErrorWithMessage()
    {   
        // Given
        var title = "Task of this day";
        var description = "Read ten page of the book Clean Code";
        var status = Status.Executing;

        var todoRaise = new TodoUpdateRequest(title, description, null, status);
    
        // When
        todoRaise.Validate(validator,errorBag);
    
        // Then
        Assert.Equal(todoRaise?.Errors?.FirstOrDefault().Value, TodoUpdateValidatorError.Todo_Category_Is_Required.AsString(EnumFormat.Description));
    }

    [Fact]
    public void WhenTodoUpdateRequestIsCreatedYourStatusIsNullShouldBeErrorWithMessage()
    {   
        // Given
        var title = "Task of this day";
        var category = ECategory.Study;
        var description = "Read ten page of the book Clean Code";

        var todoRaise = new TodoUpdateRequest(title, description, category, null);
    
        // When
        todoRaise.Validate(validator,errorBag);
    
        // Then
        var isEquals =todoRaise?.Errors?.FirstOrDefault().Value == TodoUpdateValidatorError.Todo_Status_Is_Required.AsString(EnumFormat.Description);
        Assert.True(isEquals);
    }
}