using Miniblog.App.Application.Abstractions;
using Miniblog.App.Data.Repositories;
using Miniblog.App.Domain.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Miniblog.App.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MiniblogDbContext>(options => options.UseSqlite(configuration.GetConnectionString("SqLiteDb"), o => o.UseQuerySplittingBehavior(
            QuerySplittingBehavior.SplitQuery)));

        services.AddScoped<IUnityOfWork, UnityOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();

        return services;
    }

    public static IApplicationBuilder UseDatabaseInitialization<TContext>(this IApplicationBuilder app)
            where TContext : DbContext
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

            dbContext.Database.Migrate();
        }

        return app;
    }
}
