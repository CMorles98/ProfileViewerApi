using ProfileViewer.Domain.DTOs.Base;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace ProfileViewer.Domain.Repositories
{
    public interface IRoleClaimsRepository
    {
        Task<IEnumerable<IdentityRoleClaim<Guid>>> GetAll(Expression<Func<IdentityRoleClaim<Guid>, bool>>? expression = null, bool? asNoTracking = false, PaginationFilter? pagination = null);
        Task<int> GetTotalCount(Expression<Func<IdentityRoleClaim<Guid>, bool>>? expression = null, bool? asNoTracking = false); 
        Task<IdentityRoleClaim<Guid>?> Get(Expression<Func<IdentityRoleClaim<Guid>, bool>> expression, bool? asNoTracking = false);
        Task Create(IdentityRoleClaim<Guid> entity);
        void Delete(IdentityRoleClaim<Guid> entity);
        void Update(IdentityRoleClaim<Guid> entity);
        Task CreateMultiple(IEnumerable<IdentityRoleClaim<Guid>> entityList);
        Task DeleteMultiple(IEnumerable<IdentityRoleClaim<Guid>> entityList);
        Task UpdateMultiple(IEnumerable<IdentityRoleClaim<Guid>> entityList);
        Task<bool> Exists(Expression<Func<IdentityRoleClaim<Guid>, bool>> expression);

    }
}
