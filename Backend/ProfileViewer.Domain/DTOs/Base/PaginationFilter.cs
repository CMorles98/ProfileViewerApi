namespace ProfileViewer.Domain.DTOs.Base
{
    public class PaginationFilter
    {
        public bool? AsNoTracking { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Sorting { get; set; }
        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
            Sorting = "id ASC";
        }
        public PaginationFilter(int pageNumber, int pageSize, string? sorting = null)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 0 ? pageSize : 10;
            Sorting = sorting ?? "id ASC";
        }

    }

}
