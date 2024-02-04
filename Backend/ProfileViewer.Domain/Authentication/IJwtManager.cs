using ProfileViewer.Domain.DTOs.Auth;

namespace ProfileViewer.Domain.Authentication
{
    public interface IJwtManager
    {
        public string GenerateToken(Guid id);
        public string RefreshToken(string token);
        public AuthResponseDto DeserializeToken(string token);
    }
}
