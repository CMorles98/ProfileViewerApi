using ProfileViewer.Domain.DTOs.Base;

namespace ProfileViewer.Domain.DTOs.Users
{
    public class UserDto: EntityDto
    {
        public required string Email { get; set; }

        public string? RoleName { get; set; }
    }
}
