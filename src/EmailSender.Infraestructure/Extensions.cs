using EmailSender.Core.Interfaces.Repositories;
using EmailSender.Core.Interfaces.Repositories.Common;
using EmailSender.Infraestructure.Persistence.Contex;
using EmailSender.Infraestructure.Persistence.Repositories;
using EmailSender.Infraestructure.Persistence.Repositories.Common;
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
