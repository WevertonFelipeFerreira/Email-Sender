using EmailSender.Core.Entities.Common;
using EmailSender.Core.Interfaces.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Infraestructure.Persistence.Repositories.Common
{
    public abstract class RepositoryBase<T>(DbContext context) : IRepositoryBase<T> where T : EntityBase
    {
        public virtual async Task InsertAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }
    }
}
