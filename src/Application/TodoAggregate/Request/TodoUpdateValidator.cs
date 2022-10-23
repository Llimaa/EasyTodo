
using EnumsNET;
using FluentValidation;

namespace Application.TodoAggregate.Request;

public class TodoUpdateValidator: AbstractValidator<TodoUpdateRequest>
{
    public TodoUpdateValidator()
    {
        RuleFor(_ => _.Title)
            .NotEmpty()
            .WithErrorCode(TodoUpdateValidatorError.Todo_Title_Is_Required.AsString())
            .WithMessage(TodoUpdateValidatorError.Todo_Title_Is_Required.AsString(EnumFormat.Description));

        RuleFor(_ => _.Category)
            .NotEmpty()
            .WithErrorCode(TodoUpdateValidatorError.Todo_Category_Is_Required.AsString())
            .WithMessage(TodoUpdateValidatorError.Todo_Category_Is_Required.AsString(EnumFormat.Description));
        
        RuleFor(_ => _.Description)
            .NotEmpty()
            .WithErrorCode(TodoUpdateValidatorError.Todo_Description_Is_Required.AsString())
            .WithMessage(TodoUpdateValidatorError.Todo_Description_Is_Required.AsString(EnumFormat.Description));
        
        RuleFor(_ => _.Status)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(TodoUpdateValidatorError.Todo_Status_Is_Required.AsString())
            .WithMessage(TodoUpdateValidatorError.Todo_Status_Is_Required.AsString(EnumFormat.Description));
    }
}