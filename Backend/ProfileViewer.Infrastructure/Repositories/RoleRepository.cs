using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProfileViewer.Domain.DTOs.Base;
using ProfileViewer.Domain.Entities;
using ProfileViewer.Domain.Repositories;
using ProfileViewer.Infrastructure.Context;
using ProfileViewer.Infrastructure.Repositories.Base;
using System.Linq.Expressions;

namespace ProfileViewer.Infrastructure.Repositories
{
    public class RoleRepository(ProfileViewerContext context) : RepositoryBase<Role>(context), IRoleRepository
    {
        public async Task CreateMultiple(IEnumerable<Role> entityList)
            => await AddRange(entityList);

        public async Task UpdateMultiple(IEnumerable<Role> entityList)
            => await UpdateRange(entityList);

        public async Task DeleteMultiple(IEnumerable<Role> entityList)
            => await RemoveRange(entityList);

        public async Task Create(Role entity) 
            => await Add(entity);

        public void Delete(Role entity) 
            => Remove(entity);

        public void Update(Role entity) 
            => Edit(entity);

        public async Task<IEnumerable<Role>> GetAll(Expression<Func<Role, bool>>? expression = null, bool? asNoTracking = false, PaginationFilter ? pagination = null) 
            => await Find(expression, asNoTracking, pagination).ToListAsync();

        public async Task<int> GetTotalCount(Expression<Func<Role, bool>>? expression = null, bool? asNoTracking = false)
            => await Find(expression, asNoTracking).CountAsync();

        public async Task<Role?> Get(Expression<Func<Role, bool>>? expression = null, bool? asNoTracking = false) 
            => await Find(expression, asNoTracking).FirstOrDefaultAsync();

        public async Task<bool> Exists(Expression<Func<Role, bool>> expression) 
            => await ExistsAsync(expression);
    }
}
