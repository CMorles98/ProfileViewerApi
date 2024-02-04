using ProfileViewer.Domain.DTOs.Auth;
using ProfileViewer.Domain.DTOs.Users;
using ProfileViewer.Domain.Validators.Base;

namespace ProfileViewer.Domain.Validators
{
    public interface IAuthValidator: IValidator<RegisterDto>, IValidator<RefreshTokenDto>, IValidator<LoginDto, UserDto>
    {
    }
}
