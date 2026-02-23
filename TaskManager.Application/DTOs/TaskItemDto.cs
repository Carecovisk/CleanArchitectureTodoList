namespace TaskManager.Application.DTOs;

// Usamos o 'record' porque os DTOs devem ser imutáveis. 
// Note que convertemos o Status para string para facilitar a leitura na API.
public record TaskItemDto(
    Guid Id, 
    string Title, 
    string Description, 
    string Status, 
    DateTime CreatedAt
);