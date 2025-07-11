using System.Linq.Expressions;

namespace EmailSender.Core.Interfaces.Repositories.Common
{
    public interface IRepositoryBase<T>
    {
        Task InsertAsync(T entity);
        Task<T?> FindByIdAsync(Guid id, bool track = false);
        Task<T?> FindByIdAsync(Guid id, IEnumerable<string> includes, bool track = false);
        Task<T?> SearchAsync(Expression<Func<T, bool>> filter, IEnumerable<string> includes, bool track = false);
        Task<T?> SearchAsync(Expression<Func<T, bool>> filter, bool track = false);
        Task<(IEnumerable<T> Items, long TotalItems)> PaginatedSearchAsync(int page, int pageSize, Expression<Func<T, bool>>? filter = null, IEnumerable<string>? includes =  null, bool track = false);
        Task UpdateAsync(T entity);
    }
}