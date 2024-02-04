namespace ProfileViewer.Domain.Pagination
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public required IEnumerable<T> Data { get; set; }
    }

}
