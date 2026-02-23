using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManager.Application.Behaviors;

namespace TaskManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // 1. Registra automaticamente todos os validadores do FluentValidation deste projeto
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // 2. Configura o MediatR
        services.AddMediatR(cfg => 
        {
            // Registra os Handlers
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            
            // Registra o nosso Pipeline Behavior de Validação
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return services;
    }
}