using EmailSender.Core.Entities;
using EmailSender.Core.Interfaces.Repositories;
using EmailSender.Infraestructure.Persistence.Contex;

namespace EmailSender.Infraestructure.Persistence.Repositories
{
    public class TemplateRepository(EmailSenderDbContext dbContex) : ITemplateRepository
    {
        public async Task CreateAsync(Template entity)
        {
            await dbContex.Templates.AddAsync(entity);
            await dbContex.SaveChangesAsync();
        }
    }
}
