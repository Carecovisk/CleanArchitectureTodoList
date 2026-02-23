using MediatR;

namespace TaskManager.Application.Commands;

// A interface IRequest<Guid> indica ao MediatR que este comando, 
// quando terminar de ser processado, vai devolver um Guid (o ID da nova tarefa).
public record CreateTaskCommand(string Title, string Description) : IRequest<Guid>;