using ProfileViewer.Domain.DTOs.Auth;

namespace ProfileViewer.Domain.Services
{
    public interface IAuthService
    {
        public Task<AuthResponseDto> Register(RegisterDto dto);
        public Task<AuthResponseDto> RefreshToken(RefreshTokenDto dto);
        public Task<AuthResponseDto> Login(LoginDto dto);
    }
}
