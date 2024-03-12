namespace api.Helpers
{
    public class QueryObject
    {
        public string? SortBy { get; set; } = null;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
