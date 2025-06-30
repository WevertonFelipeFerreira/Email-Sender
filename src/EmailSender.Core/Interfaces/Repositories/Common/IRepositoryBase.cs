using EmailSender.Core.Entities.Common;

namespace EmailSender.Core.Interfaces.Repositories.Common
{
    public interface IRepositoryBase<in T>
    {
        Task InsertAsync(T entity);
    }
}
