namespace backend.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedList(IEnumerable<T> items, int currentPage, int pageSize, int count)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;

            AddRange(items);
        }

        public PagedList(IEnumerable<T> items, int totalPages, int currentPage, int pageSize, int count)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalCount = count;

            AddRange(items);
        }
    }
}