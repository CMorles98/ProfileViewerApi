using ProfileViewer.Domain.DTOs.Base;

namespace ProfileViewer.Domain.DTOs.Users
{
    public class UserFiltersDto : PaginationFilter
    {
        public string? Email { get; set; }
    }
}
