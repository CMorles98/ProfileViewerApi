using ProfileViewer.Domain.DTOs.Base;
using ProfileViewer.Domain.Repositories;
using ProfileViewer.Infrastructure.Context;
using ProfileViewer.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace ProfileViewer.Infrastructure.Repositories
{
    public class UserRoleRepository(ProfileViewerContext context) : RepositoryBase<IdentityUserRole<Guid>>(context), IUserRoleRepository
    {
        public async Task CreateMultiple(IEnumerable<IdentityUserRole<Guid>> entityList) 
            => await AddRange(entityList);
        
        public async Task UpdateMultiple(IEnumerable<IdentityUserRole<Guid>> entityList) 
            => await UpdateRange(entityList);
        
        public async Task DeleteMultiple(IEnumerable<IdentityUserRole<Guid>> entityList) 
            => await RemoveRange(entityList);
        
        public async Task Create(IdentityUserRole<Guid> entity) 
            => await Add(entity);

        public void Delete(IdentityUserRole<Guid> entity) 
            => Remove(entity);

        public void Update(IdentityUserRole<Guid> entity) 
            => Edit(entity);

        public async Task<IEnumerable<IdentityUserRole<Guid>>> GetAll(Expression<Func<IdentityUserRole<Guid>, bool>>? expression = null, bool? asNoTracking = false, PaginationFilter ? pagination = null) 
            => await Find(expression, asNoTracking, pagination).ToListAsync();

        public async Task<int> GetTotalCount(Expression<Func<IdentityUserRole<Guid>, bool>>? expression = null, bool? asNoTracking = false)
            => await Find(expression, asNoTracking).CountAsync();

        public async Task<IdentityUserRole<Guid>?> Get(Expression<Func<IdentityUserRole<Guid>, bool>>? expression = null, bool? asNoTracking = false) 
            => await Find(expression, asNoTracking).FirstOrDefaultAsync();

        public async Task<bool> Exists(Expression<Func<IdentityUserRole<Guid>, bool>> expression)
            => await ExistsAsync(expression);
    }
}
