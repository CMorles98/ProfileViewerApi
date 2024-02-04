using Microsoft.EntityFrameworkCore;
using ProfileViewer.Domain.DTOs.Base;
using ProfileViewer.Domain.Entities;
using ProfileViewer.Domain.Repositories;
using ProfileViewer.Infrastructure.Context;
using ProfileViewer.Infrastructure.Repositories.Base;
using System.Linq.Expressions;

namespace ProfileViewer.Infrastructure.Repositories
{
    public class UserRepository(ProfileViewerContext context) : RepositoryBase<User>(context), IUserRepository
    {
        public async Task CreateMultiple(IEnumerable<User> entityList)
            => await AddRange(entityList);

        public async Task UpdateMultiple(IEnumerable<User> entityList)
            => await UpdateRange(entityList);

        public async Task DeleteMultiple(IEnumerable<User> entityList)
            => await RemoveRange(entityList);

        public async Task Create(User entity) 
            => await Add(entity);

        public void Delete(User entity) 
            => Remove(entity);

        public void Update(User entity) 
            => Edit(entity);

        public async Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>>? expression = null, bool? asNoTracking = false, PaginationFilter ? pagination = null) 
            => await Find(expression, asNoTracking, pagination).ToListAsync();

        public async Task<int> GetTotalCount(Expression<Func<User, bool>>? expression = null, bool? asNoTracking = false)
            => await Find(expression, asNoTracking).CountAsync();

        public async Task<User?> Get(Expression<Func<User, bool>>? expression = null, bool? asNoTracking = false) 
            => await Find(expression, asNoTracking).FirstOrDefaultAsync();

        public async Task<bool> Exists(Expression<Func<User, bool>> expression)
            => await ExistsAsync(expression);
    }
}
