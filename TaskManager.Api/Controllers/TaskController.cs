using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Commands;
using TaskManager.Application.Queries;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // A rota será: /api/tasks
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    // Injetamos apenas o IMediator. O Controller não sabe o que é Repositório ou DbContext!
    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: /api/tasks
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
    {
        // O MediatR pega o comando, passa pelo validador e envia para o Handler
        var taskId = await _mediator.Send(command);
        
        // Retorna HTTP 201 (Created) informando o ID da nova tarefa
        return Created(string.Empty, new { id = taskId }); 
    }

    // GET: /api/tasks
    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        // Instanciamos a query
        var query = new GetAllTasksQuery();
        
        // O MediatR busca os dados e já nos devolve a lista de DTOs
        var tasks = await _mediator.Send(query);
        
        // Retorna HTTP 200 (OK) com a lista em formato JSON
        return Ok(tasks);
    }
}