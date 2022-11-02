using Application.ErrorBag;
using Application.Extensions;
using FluentValidation;

namespace Application.TodoAggregate.Request;

public class TodoRaiseRequest
{
    public TodoRaiseRequest(string? title, string? description, ECategory? category)
    {
        Title = title;
        Description = description;
        Category = category;
    }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public ECategory? Category { get; set; }

    public Dictionary<string, string>? Errors { get; private set; }
    public bool Valid { get; private set; }

   public void Validate(IValidator<TodoRaiseRequest> validator, IErrorBagHandler errorHandler) 
    {
        var (errors, valid) = validator.Validate(this);
        Errors = errors.AsDefaultFormat();
        if(Errors.Count > 0)
            errorHandler.HandlerError(Errors);
        Valid = valid;
    }
}