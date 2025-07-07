using EmailSender.Core.Entities.Common;

namespace EmailSender.Core.Interfaces.Repositories.Common
{
    public interface IRepositoryBase<T>
    {
        Task InsertAsync(T entity);
        Task<T?> FindByIdAsync(Guid id, bool track = false);
        Task<T?> FindByIdAsync(Guid id, IEnumerable<string> includes, bool track = false);
    }
}
