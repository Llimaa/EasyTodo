
using EnumsNET;
using FluentValidation;

namespace Application.TodoAggregate.Request;

public class TodoRaiseValidator: AbstractValidator<TodoRaiseRequest>
{
    public TodoRaiseValidator()
    {
        RuleFor(_ => _.Title)
            .NotEmpty()
            .WithErrorCode(TodoRaiseValidatorError.Todo_Title_Is_Required.AsString())
            .WithMessage(TodoRaiseValidatorError.Todo_Title_Is_Required.AsString(EnumFormat.Description));

        RuleFor(_ => _.Category)
            .NotEmpty()
            .WithErrorCode(TodoRaiseValidatorError.Todo_Category_Is_Required.AsString())
            .WithMessage(TodoRaiseValidatorError.Todo_Category_Is_Required.AsString(EnumFormat.Description));
        
        RuleFor(_ => _.Description)
            .NotEmpty()
            .WithErrorCode(TodoRaiseValidatorError.Todo_Description_Is_Required.AsString())
            .WithMessage(TodoRaiseValidatorError.Todo_Description_Is_Required.AsString(EnumFormat.Description));
    }
}