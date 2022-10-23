
using System.ComponentModel;

namespace Application.TodoAggregate.Request;

public enum TodoUpdateValidatorError 
{
    [Description("Todo Title is required")]
    Todo_Title_Is_Required,

    [Description("Todo Description is required")]
    Todo_Description_Is_Required,

    [Description("Todo Category is required")]
    Todo_Category_Is_Required,
    
    [Description("Todo Status is required")]
    Todo_Status_Is_Required,
}