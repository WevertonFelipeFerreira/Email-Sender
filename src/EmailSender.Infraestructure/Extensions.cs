using EmailSender.Core.Interfaces.Repositories;
using EmailSender.Infraestructure.Persistence.Contex;
using EmailSender.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSender.Infraestructure
{
    public static class Extensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped<ISenderRepository, SenderRepository>();

            return services;
        }

        public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration cfg)
        {
            return services.AddDbContext<EmailSenderDbContext>(options =>
                    options.UseSqlServer(cfg.GetConnectionString("DbConnection"))
                );
        }
    }
}
