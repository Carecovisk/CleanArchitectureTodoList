using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Enums.TaskStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Construtor para criar uma nova tarefa
    public TaskItem(string title, string description)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Status = Enums.TaskStatus.Todo;
        CreatedAt = DateTime.UtcNow;
    }

    // Método para atualizar o status (Regra de Negócio)
    public void UpdateStatus(Enums.TaskStatus newStatus)
    {
        Status = newStatus;
    }
}