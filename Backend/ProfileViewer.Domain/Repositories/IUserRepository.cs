using ProfileViewer.Domain.DTOs.Base;
using ProfileViewer.Domain.Entities;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace ProfileViewer.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>>? expression = null, bool? asNoTracking = false, PaginationFilter? pagination = null);
        Task<int> GetTotalCount(Expression<Func<User, bool>>? expression = null, bool? asNoTracking = false); 
        Task<User?> Get(Expression<Func<User, bool>> expression, bool? asNoTracking = false);
        Task Create(User entity);
        void Delete(User entity);
        void Update(User entity);
        Task CreateMultiple(IEnumerable<User> entityList);
        Task DeleteMultiple(IEnumerable<User> entityList);
        Task UpdateMultiple(IEnumerable<User> entityList);
        Task<bool> Exists(Expression<Func<User, bool>> expression);

    }
}
