namespace ProfileViewer.Domain.DTOs.Auth
{
    public class AuthResponseDto
    {
        public AuthResponseDto(bool success, string? token = null, Guid? userId = null)
        {
            Success = success;
            UserId = userId;
            Token = token;
        }
        public bool Success { get; set; }
        public string? Token { get; set; }
        public Guid? UserId { get; set; }
    }
}
