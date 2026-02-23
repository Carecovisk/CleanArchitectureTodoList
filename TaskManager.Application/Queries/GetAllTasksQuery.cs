using MediatR;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Queries;

// Esta query não precisa de parâmetros, pois vamos buscar todas as tarefas.
// O IRequest indica que, ao ser executada, devolverá uma lista de TaskItemDto.
public record GetAllTasksQuery() : IRequest<IEnumerable<TaskItemDto>>;