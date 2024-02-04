using ProfileViewer.Domain.DTOs.Base;
using System.Linq.Expressions;
using ProfileViewer.Domain.Entities;

namespace ProfileViewer.Domain.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAll(Expression<Func<Role, bool>>? expression = null, bool? asNoTracking = false, PaginationFilter? pagination = null);
        Task<int> GetTotalCount(Expression<Func<Role, bool>>? expression = null, bool? asNoTracking = false); 
        Task<Role?> Get(Expression<Func<Role, bool>> expression, bool? asNoTracking = false);
        Task Create(Role entity);
        void Delete(Role entity);
        void Update(Role entity);
        Task CreateMultiple(IEnumerable<Role> entityList);
        Task DeleteMultiple(IEnumerable<Role> entityList);
        Task UpdateMultiple(IEnumerable<Role> entityList);
        Task<bool> Exists(Expression<Func<Role, bool>> expression);

    }
}
