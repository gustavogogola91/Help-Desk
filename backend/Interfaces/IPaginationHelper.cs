using backend.Helpers;

namespace backend.Interfaces
{
    public interface IPaginationHelper
    {
        Task<PagedList<T>> CreateAsync<T>(IQueryable<T> source, int pageNumber, int pageSize) where T : class;
    }
}