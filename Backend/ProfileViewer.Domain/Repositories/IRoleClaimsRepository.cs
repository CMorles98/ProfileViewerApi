using ProfileViewer.Domain.DTOs.Base;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace ProfileViewer.Domain.Repositories
{
    public interface IUserClaimsRepository
    {
        Task<IEnumerable<IdentityUserClaim<Guid>>> GetAll(Expression<Func<IdentityUserClaim<Guid>, bool>>? expression = null, bool? asNoTracking = false, PaginationFilter? pagination = null);
        Task<int> GetTotalCount(Expression<Func<IdentityUserClaim<Guid>, bool>>? expression = null, bool? asNoTracking = false); 
        Task<IdentityUserClaim<Guid>?> Get(Expression<Func<IdentityUserClaim<Guid>, bool>> expression, bool? asNoTracking = false);
        Task Create(IdentityUserClaim<Guid> entity);
        void Delete(IdentityUserClaim<Guid> entity);
        void Update(IdentityUserClaim<Guid> entity);
        Task CreateMultiple(IEnumerable<IdentityUserClaim<Guid>> entityList);
        Task DeleteMultiple(IEnumerable<IdentityUserClaim<Guid>> entityList);
        Task UpdateMultiple(IEnumerable<IdentityUserClaim<Guid>> entityList);
        Task<bool> Exists(Expression<Func<IdentityUserClaim<Guid>, bool>> expression);


    }
}
