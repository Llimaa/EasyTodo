using Application.ErrorBag;
using Application.Queries;
using Application.TodoAggregate;
using Application.TodoAggregate.Request;
using FluentValidation;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("todo")]
public class TodoController: ControllerBase 
{
    private readonly ITodoRepository todoRepository;
    private readonly ITodoDbContext todoDbContext;
    private readonly ISearchTodoRepository searchTodoRepository;
    private readonly IValidator<TodoRaiseRequest> validatorRaiseRequest;
    private readonly IErrorBagHandler errorBag;
    private readonly IValidator<TodoUpdateRequest> validatorUpdateRequest;
    public TodoController(
        ITodoRepository todoRepository,
        ISearchTodoRepository searchTodoRepository,
        ITodoDbContext todoDbContext,
        IValidator<TodoRaiseRequest> validatorRaiseRequest,
        IValidator<TodoUpdateRequest> validatorUpdateRequest,
        IErrorBagHandler errorBag
    ){
        this.todoRepository = todoRepository;      
        this.searchTodoRepository = searchTodoRepository;
        this.todoDbContext = todoDbContext;
        this.validatorUpdateRequest = validatorUpdateRequest;
        this.validatorRaiseRequest = validatorRaiseRequest;
        this.errorBag = errorBag;
    }

    [HttpGet()]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SearchTodoResponse>))]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await searchTodoRepository.GetAll());
    }

    [HttpGet("/{id:Guid}")]
    [ProducesResponseType(200, Type = typeof(SearchTodoResponse))]
    public async Task<IActionResult> GetById(Guid id) 
    {
        return Ok(await searchTodoRepository.GetById(id));
    }
    
    [HttpGet("/{category:int}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SearchTodoResponse>))]
    public async Task<IActionResult> GetByCategory(ECategory category) 
    {
        return Ok(await searchTodoRepository.GetAllByCategory(category));
    }

    [HttpPost()]
    [ProducesResponseType(204, Type = typeof(Guid))]
    public async Task<IActionResult> Raise([FromBody] TodoRaiseRequest todoRequest) 
    {
        todoRequest.Validate(validatorRaiseRequest, errorBag);
        try
        {
            await todoDbContext.StartTransactionAsync();
            var id = await todoRepository.Raise(Todo.BindingToTodo(todoRequest));
            await todoDbContext.CommitTransactionAsync();

            return CreatedAtAction(nameof(GetById), new {Id = id}, todoRequest);
        }
        catch
        {
            await todoDbContext.AbortTransactionAsync();
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> Update(Guid id, [FromBody] TodoUpdateRequest todoRequest) 
    {
        todoRequest.Validate(validatorUpdateRequest, errorBag);
        try
        {
            await todoDbContext.StartTransactionAsync();
            await todoRepository.Update(todoRequest, id);
            await todoDbContext.CommitTransactionAsync();

            return NoContent();
        }
        catch
        {
            await todoDbContext.AbortTransactionAsync();
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) 
    {
        try
        {
            await todoDbContext.StartTransactionAsync();
            await todoRepository.Remove(id);
            await todoDbContext.CommitTransactionAsync();
            return NoContent();
        }
        catch
        {
            await todoDbContext.AbortTransactionAsync();
            return BadRequest();
        }
    }
}