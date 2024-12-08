using Miniblog.App.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Miniblog.App.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(ApplicationAssembly.Assembly);
        });

        return services;
    }
}
