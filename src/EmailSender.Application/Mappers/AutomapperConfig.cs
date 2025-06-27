using EmailSender.Application.Mappers.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSender.Application.Mappers
{
    public static class AutomapperConfig
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(
                    typeof(CommandToEntity)
                );
        }
    }
}
