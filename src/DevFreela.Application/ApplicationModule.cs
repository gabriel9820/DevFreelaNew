using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Models;
using DevFreela.Application.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace DevFreela.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddHandlers()
            .AddValidation();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CreateProjectCommand>());
        services.AddTransient<IPipelineBehavior<CreateProjectCommand, ResultViewModel<int>>, CreateProjectCommandBehavior>();

        return services;
    }

    private static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssemblyContaining<CreateProjectValidator>()
            .AddFluentValidationAutoValidation();

        return services;
    }
}
