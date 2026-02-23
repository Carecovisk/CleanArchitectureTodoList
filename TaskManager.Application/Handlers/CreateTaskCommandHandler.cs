using MediatR;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Application.Commands;

namespace TaskManager.Application.Handlers;

// Esta classe implementa a interface dizendo: "Eu sei lidar com um CreateTaskCommand e devolvo um Guid"
public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly ITaskRepository _repository;

    // Injetamos a interface do repositório (o contrato que definimos no Domínio)
    public CreateTaskCommandHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // 1. Criar a entidade do domínio com os dados que vieram no pedido
        var task = new TaskItem(request.Title, request.Description);

        // 2. Guardar na base de dados (a Aplicação não sabe que é Postgres, apenas usa o contrato!)
        await _repository.AddAsync(task);

        // 3. Devolver o ID da tarefa gerada
        return task.Id;
    }
}