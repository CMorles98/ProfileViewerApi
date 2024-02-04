
using ProfileViewer.Domain.DTOs.Users;
using ProfileViewer.Domain.Pagination;

namespace ProfileViewer.Domain.Services
{
    public interface IUserService
    {
        Task<PagedResult<ListUserDto>> GetAll(UserFiltersDto filters);
        Task<UserDto> GetById(Guid id);
        Task Delete(Guid id);
        Task<UserDto> Update(EditUserDto dto, Guid id);
    }
}
