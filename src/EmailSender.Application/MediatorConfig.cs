using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EmailSender.Application
{
    public static class MediatorConfig
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
