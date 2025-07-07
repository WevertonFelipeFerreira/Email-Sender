using EmailSender.Core.Entities.Common;
using EmailSender.Core.Interfaces.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Infraestructure.Persistence.Repositories.Common
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _context;
        protected RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> FindByIdAsync(Guid id, bool track = false)
        {
            if (!track)
            {
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T?> FindByIdAsync(Guid id, IEnumerable<string> includes, bool track = false)
        {
            IQueryable<T> query = _dbSet;

            if (!track)
            {
                query = query.AsNoTracking();
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
