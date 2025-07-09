using EmailSender.Core.Entities.Common;
using EmailSender.Core.Interfaces.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public virtual async Task<T?> SearchAsync(Expression<Func<T, bool>> filter, IEnumerable<string> includes, bool track = false)
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

            return await query.FirstOrDefaultAsync(filter);
        }

        public virtual async Task<T?> SearchAsync(Expression<Func<T, bool>> filter, bool track = false)
        {
            if (!track)
            {
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }

            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
