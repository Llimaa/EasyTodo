using Application.ErrorBag;
using Application.Extensions;
using FluentValidation;

namespace Application.TodoAggregate;

public class TodoUpdateRequest
{
    public TodoUpdateRequest(string? title, string? description, ECategory? category, Status? status)
    {
        Title = title;
        Description = description;
        Category = category;
        Status = status;
    }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public ECategory? Category { get; set; }
    public Status? Status { get; set; }

    public Dictionary<string, string>? Errors { get; private set; }
    public bool Valid { get; private set; }

    public void Validate(IValidator<TodoUpdateRequest> validator, IErrorBagHandler errorHandler) 
    {
        var (errors, valid) = validator.Validate(this);
        Errors = errors.AsDefaultFormat();
        if(Errors.Count > 0)
            errorHandler.HandlerError(Errors);
        Valid = valid;
    }
}