using Microsoft.Extensions.DependencyInjection;
using Tacomanda.Application.Respuestas;
using Tacomanda.Application.Validacion;
using Tacomanda.Application.CrearComanda;
using Tacomanda.Application.InicioSesion;
using MediatR;
using AutoMapper;

namespace Tacomanda.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(ConfigurarMediatR);
        services.AddAutoMapper(ConfigurarAutoMapper, typeof(ComandaMappingProfile));
        services.AddTransient<IValidator<IniciarSesionCommand>, IniciarSesionValidator>();
        services.AddTransient<IValidator<CrearComandaCommand>, CrearComandaValidator>();
        return services;
    }

    private static void ConfigurarMediatR(MediatRServiceConfiguration configuration)
    {
        configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
    }

    private static void ConfigurarAutoMapper(IMapperConfigurationExpression configuration)
    {
        configuration.LicenseKey = Environment.GetEnvironmentVariable("AUTOMAPPER_LICENSE_KEY");
    }
}
