using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceProvider)
    {
        serviceProvider.AddMediatR(Assembly.GetExecutingAssembly());
        return serviceProvider;
    }
}
