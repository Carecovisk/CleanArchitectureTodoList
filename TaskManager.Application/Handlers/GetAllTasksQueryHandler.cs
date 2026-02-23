using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Application.Queries;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Handlers;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskItemDto>>
{
    private readonly ITaskRepository _repository;

    public GetAllTasksQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskItemDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        // 1. Vai à base de dados buscar as entidades de domínio
        var tasks = await _repository.GetAllAsync();

        // 2. Usa o LINQ (método Select) para transformar cada TaskItem num TaskItemDto
        var taskDtos = tasks.Select(task => new TaskItemDto(
            task.Id,
            task.Title,
            task.Description,
            task.Status.ToString(), // Converte o Enum para o seu nome em texto (ex: "Todo")
            task.CreatedAt
        ));

        // 3. Devolve a lista pronta para a API
        return taskDtos;
    }
}