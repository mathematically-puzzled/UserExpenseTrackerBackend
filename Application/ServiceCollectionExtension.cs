using Application.Pipeline_Behaviour;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services
                // Adding Automapper Dependency.
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                // Add MediatR Dependency.
                .AddMediatR(Assembly.GetExecutingAssembly())
                // Add Validator Dependency.
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                // Add Pipeline Transient Property.
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorPipelineBehaviour<,>));
        }
    }
}
