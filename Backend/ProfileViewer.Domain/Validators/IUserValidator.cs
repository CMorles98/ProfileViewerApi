using ProfileViewer.Domain.DTOs.Users;
using ProfileViewer.Domain.Entities;
using ProfileViewer.Domain.Validators.Base;

namespace ProfileViewer.Domain.Validators
{
    public interface IUserValidator: IValidator<EditUserDto,User,Guid>, IValidator<Guid,UserDto>
    {
    }
}
