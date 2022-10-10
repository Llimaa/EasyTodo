using Application.Queries;
using Application.TodoAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("todo")]
public class TodoController: ControllerBase 
{
    private readonly ITodoRepository todoRepository;
    private readonly ISearchTodoRepository searchTodoRepository;
    public TodoController(ITodoRepository todoRepository, ISearchTodoRepository searchTodoRepository)
    {
        this.todoRepository = todoRepository;      
        this.searchTodoRepository = searchTodoRepository;      
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
    public async Task<IActionResult> GetByDate(string date) 
    {
        return Ok(await searchTodoRepository.GetAllByDate(DateOnly.Parse(date)));
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
        var id = await todoRepository.Raise(Todo.BindingToTodo(todoRequest));
        return CreatedAtAction(nameof(GetById), new {Id = id}, todoRequest);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> Update(Guid id, [FromBody] TodoRequestUpdate todoRequestUpdate) 
    {
        await todoRepository.Update(todoRequestUpdate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) 
    {
        await todoRepository.Remove(id);
        return NoContent();
    }
}