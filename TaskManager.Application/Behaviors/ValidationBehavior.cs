using FluentValidation;
using MediatR;

namespace TaskManager.Application.Behaviors;

// Esta classe intercepta qualquer Request (Comando ou Query) e a sua respectiva Resposta
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    // O .NET vai injetar aqui todos os validadores que existirem para o TRequest atual
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next(); // Se não tem validador para esse comando, deixa passar
        }

        var context = new ValidationContext<TRequest>(request);

        // Executa todas as validações de forma assíncrona
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        // Pega todos os erros encontrados
        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count != 0)
        {
            // Se houver erros, interrompe o fluxo e lança uma exceção do FluentValidation
            throw new ValidationException(failures);
        }

        // Se passou em tudo, chama o próximo passo (que será o Handler ou o próximo Behavior)
        return await next();
    }
}