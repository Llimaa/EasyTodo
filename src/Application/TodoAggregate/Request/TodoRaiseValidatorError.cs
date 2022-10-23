
using System.ComponentModel;

namespace Application.TodoAggregate.Request;

public enum TodoRaiseValidatorError 
{
    [Description("Todo Title is required")]
    Todo_Title_Is_Required,

    [Description("Todo Description is required")]
    Todo_Description_Is_Required,

    [Description("Todo Category is required")]
    Todo_Category_Is_Required,
}