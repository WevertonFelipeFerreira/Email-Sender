using EmailSender.Application.Services;
using EmailSender.Application.Services.Interfaces;
using EmailSender.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EmailSender.Application
{
    public static class Extension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordProtector>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<AuthKeysOptions>>();
                return new AesEncryptService(options.Value.SenderKey!);
            });

            return services;
        }
    }
}
