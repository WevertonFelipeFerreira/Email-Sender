using EmailSender.Core.Entities;
using EmailSender.Core.Interfaces.Repositories;
using EmailSender.Infraestructure.Persistence.Contex;
using EmailSender.Infraestructure.Persistence.Repositories.Common;

namespace EmailSender.Infraestructure.Persistence.Repositories
{
    public class AttributeRepository : RepositoryBase<AttributeEntity>, IAttributeRepository
    {
        public AttributeRepository(EmailSenderDbContext context) : base(context)
        {

        }
    }
}
