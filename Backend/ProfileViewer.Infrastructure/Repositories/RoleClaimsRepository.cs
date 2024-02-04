using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProfileViewer.Domain.DTOs.Base;
using ProfileViewer.Domain.Repositories;
using ProfileViewer.Infrastructure.Context;
using ProfileViewer.Infrastructure.Repositories.Base;
using System.Linq.Expressions;

namespace ProfileViewer.Infrastructure.Repositories
{
    public class RoleClaimsRepository(ProfileViewerContext context) : RepositoryBase<IdentityRoleClaim<Guid>>(context), IRoleClaimsRepository
    {
        public async Task CreateMultiple(IEnumerable<IdentityRoleClaim<Guid>> entityList)
            => await AddRange(entityList);

        public async Task UpdateMultiple(IEnumerable<IdentityRoleClaim<Guid>> entityList)
            => await UpdateRange(entityList);

        public async Task DeleteMultiple(IEnumerable<IdentityRoleClaim<Guid>> entityList)
            => await RemoveRange(entityList);

        public async Task Create(IdentityRoleClaim<Guid> entity) 
            => await Add(entity);

        public void Delete(IdentityRoleClaim<Guid> entity) 
            => Remove(entity);

        public void Update(IdentityRoleClaim<Guid> entity) 
            => Edit(entity);

        public async Task<IEnumerable<IdentityRoleClaim<Guid>>> GetAll(Expression<Func<IdentityRoleClaim<Guid>, bool>>? expression = null, bool? asNoTracking = false, PaginationFilter ? pagination = null) 
            => await Find(expression, asNoTracking, pagination).ToListAsync();

        public async Task<int> GetTotalCount(Expression<Func<IdentityRoleClaim<Guid>, bool>>? expression = null, bool? asNoTracking = false)
            => await Find(expression, asNoTracking).CountAsync();

        public async Task<IdentityRoleClaim<Guid>?> Get(Expression<Func<IdentityRoleClaim<Guid>, bool>>? expression = null, bool? asNoTracking = false) 
            => await Find(expression, asNoTracking).FirstOrDefaultAsync();

        public async Task<bool> Exists(Expression<Func<IdentityRoleClaim<Guid>, bool>> expression)
            => await ExistsAsync(expression);
    }
}
