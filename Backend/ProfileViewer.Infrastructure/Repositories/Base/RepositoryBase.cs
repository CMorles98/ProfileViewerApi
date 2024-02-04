using ProfileViewer.Domain.DTOs.Base;
using ProfileViewer.Domain.Repositories.Base;
using ProfileViewer.Infrastructure.Context;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ProfileViewer.Infrastructure.Repositories.Base
{
    public class RepositoryBase<T>(ProfileViewerContext context) : IRepositoryBase<T> where T : class
    {
        private readonly ProfileViewerContext _context = context;

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression) => 
            await _context.Set<T>().AsNoTracking().AnyAsync(expression);

        public Task UpdateRange(IEnumerable<T> entityList)
        {
            _context.Set<T>().UpdateRange(entityList);
            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<T> entityList)
        {
            _context.Set<T>().RemoveRange(entityList);
            return Task.CompletedTask;
        }

        public async Task AddRange(IEnumerable<T> entityList) => await _context.Set<T>().AddRangeAsync(entityList);

        public async Task Add(T entity) => await _context.Set<T>().AddAsync(entity);

        public IQueryable<T> Find(Expression<Func<T, bool>>? expression = null, bool? asNoTracking = false, PaginationFilter? pagination = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (expression is not null)
                query = query.Where(expression);

            if (asNoTracking is true)
                query = query.AsNoTracking();

            if (pagination is not null)
                query = query
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                    .Take(pagination.PageSize)
                    .OrderBy(pagination.Sorting ?? "Id ASC");

            return query;

        }

        public void Remove(T entity) => _context.Set<T>().Remove(entity);

        public void Edit(T entity) => _context.Set<T>().Update(entity);

    }
}
