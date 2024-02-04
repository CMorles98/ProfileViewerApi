using ProfileViewer.Domain.DTOs.Base;
using ProfileViewer.Domain.Repositories;
using ProfileViewer.Infrastructure.Context;
using ProfileViewer.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace ProfileViewer.Infrastructure.Repositories
{
    public class UserClaimsRepository(ProfileViewerContext context) : RepositoryBase<IdentityUserClaim<Guid>>(context), IUserClaimsRepository
    {
        public async Task CreateMultiple(IEnumerable<IdentityUserClaim<Guid>> entityList)
            => await AddRange(entityList);

        public async Task UpdateMultiple(IEnumerable<IdentityUserClaim<Guid>> entityList)
            => await UpdateRange(entityList);

        public async Task DeleteMultiple(IEnumerable<IdentityUserClaim<Guid>> entityList)
            => await RemoveRange(entityList);

        public async Task Create(IdentityUserClaim<Guid> entity) 
            => await Add(entity);

        public void Delete(IdentityUserClaim<Guid> entity) 
            => Remove(entity);

        public void Update(IdentityUserClaim<Guid> entity) 
            => Edit(entity);

        public async Task<IEnumerable<IdentityUserClaim<Guid>>> GetAll(Expression<Func<IdentityUserClaim<Guid>, bool>>? expression = null, bool? asNoTracking = false, PaginationFilter ? pagination = null) 
            => await Find(expression, asNoTracking, pagination).ToListAsync();

        public async Task<int> GetTotalCount(Expression<Func<IdentityUserClaim<Guid>, bool>>? expression = null, bool? asNoTracking = false)
            => await Find(expression, asNoTracking).CountAsync();

        public async Task<IdentityUserClaim<Guid>?> Get(Expression<Func<IdentityUserClaim<Guid>, bool>>? expression = null, bool? asNoTracking = false) 
            => await Find(expression, asNoTracking).FirstOrDefaultAsync();

        public async Task<bool> Exists(Expression<Func<IdentityUserClaim<Guid>, bool>> expression)
            => await ExistsAsync(expression);
    }
}
