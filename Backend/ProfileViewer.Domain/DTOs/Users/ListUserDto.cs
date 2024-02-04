using ProfileViewer.Domain.DTOs.Base;

namespace ProfileViewer.Domain.DTOs.Users
{
    public class ListUserDto: EntityDto
    {
        public required string Email { get; set; }

    }
}
