using EmailSender.Core.Entities;

namespace EmailSender.Core.Interfaces.Repositories
{
    public interface ITemplateRepository
    {
        Task CreateAsync(Template entity);
    }
}
