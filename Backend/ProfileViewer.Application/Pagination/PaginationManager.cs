using ProfileViewer.Domain.Pagination;

namespace ProfileViewer.Application.Pagination
{
    public static class PaginationManager<T>
    {
        public static PagedResult<T> CreatePagedResult(IEnumerable<T> list, int totalCount, int page, int pageSize) 
            => new()
            {
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            Data = list
        };
    }
}
