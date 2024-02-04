using ProfileViewer.Domain.DTOs.Base;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace ProfileViewer.Domain.Repositories
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<IdentityUserRole<Guid>>> GetAll(Expression<Func<IdentityUserRole<Guid>, bool>>? expression = null, bool? asNoTracking = false, PaginationFilter? pagination = null);
        Task<int> GetTotalCount(Expression<Func<IdentityUserRole<Guid>, bool>>? expression = null, bool? asNoTracking = false); 
        Task<IdentityUserRole<Guid>?> Get(Expression<Func<IdentityUserRole<Guid>, bool>> expression, bool? asNoTracking = false);
        Task Create(IdentityUserRole<Guid> entity);
        void Delete(IdentityUserRole<Guid> entity);
        void Update(IdentityUserRole<Guid> entity);
        Task CreateMultiple(IEnumerable<IdentityUserRole<Guid>> entityList);
        Task DeleteMultiple(IEnumerable<IdentityUserRole<Guid>> entityList);
        Task UpdateMultiple(IEnumerable<IdentityUserRole<Guid>> entityList);
        Task<bool> Exists(Expression<Func<IdentityUserRole<Guid>, bool>> expression);

    }
}
