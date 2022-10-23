using Application.Queries;
using Application.TodoAggregate;
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
    public TodoController(ITodoRepository todoRepository, ISearchTodoRepository searchTodoRepository,ITodoDbContext todoDbContext)
    {
        this.todoRepository = todoRepository;      
        this.searchTodoRepository = searchTodoRepository;
        this.todoDbContext = todoDbContext;     
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

    [HttpGet("/{date}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SearchTodoResponse>))]
    public async Task<IActionResult> GetByDate(DateTime date) 
    {
        return Ok(await searchTodoRepository.GetAllByDate(date));
    }

    [HttpGet("/{category:int}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SearchTodoResponse>))]
    public async Task<IActionResult> GetByCategory(ECategory category) 
    {
        return Ok(await searchTodoRepository.GetAllByCategory(category));
    }

    [HttpPost()]
    [ProducesResponseType(204, Type = typeof(Guid))]
    public async Task<IActionResult> Raise([FromBody] TodoRequestRaise todoRequest) 
    {
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
    public async Task<IActionResult> Update(Guid id, [FromBody] TodoRequestUpdate todoRequestUpdate) 
    {
        try
        {
            await todoDbContext.StartTransactionAsync();
            await todoRepository.Update(todoRequestUpdate, id);
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