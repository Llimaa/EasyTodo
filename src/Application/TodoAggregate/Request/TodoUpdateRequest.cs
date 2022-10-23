using Application.ErrorBag;
using Application.Extensions;
using FluentValidation;

namespace Application.TodoAggregate;

public class TodoUpdateRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ECategory? Category { get; set; }
    public Status Status { get; set; }

    public Dictionary<string, string>? Errors { get; private set; }
    public bool Valid { get; private set; }

    public void Validate(IValidator<TodoUpdateRequest> validator, IErrorBagHandler errorHandler) 
    {
        var (errors, valid) = validator.Validate(this);
        Errors = errors.AsDefaultFormat();
        if(Errors is not null)
            errorHandler.HandlerError(Errors);
        Valid = valid;
    }
}