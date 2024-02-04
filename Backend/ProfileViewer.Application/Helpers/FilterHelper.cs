namespace ProfileViewer.Application.Helpers
{
    public static class FilterHelpers
    {
        public static bool FilterDateByRange(DateTime? StartDate, DateTime? EndDate, DateTime DateToFilter) 
            => (!StartDate.HasValue && !EndDate.HasValue) ||
            (!StartDate.HasValue && EndDate.HasValue && EndDate.Value.Date >= DateToFilter.Date) ||
            (StartDate.HasValue && !EndDate.HasValue && StartDate.Value.Date <= DateToFilter.Date) ||
            (StartDate.HasValue && EndDate.HasValue && StartDate.Value.Date <= DateToFilter.Date && EndDate.Value.Date >= DateToFilter.Date);
    }
}
