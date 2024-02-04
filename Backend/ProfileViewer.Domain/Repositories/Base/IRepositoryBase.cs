using ProfileViewer.Domain.DTOs.Base;
using System.Linq.Expressions;

namespace ProfileViewer.Domain.Repositories.Base
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> Find(Expression<Func<T, bool>>? expression = null, bool? asNoTracking = false, PaginationFilter? pagination = null);
        Task Add(T entity);
        void Edit(T entity);
        void Remove(T entity);
    }
}
