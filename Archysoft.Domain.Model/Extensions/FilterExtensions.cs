using System.Linq;
using System.Linq.Dynamic.Core;
using Archysoft.Data.Enteties;
using Archysoft.Domain.Model.Model.Common;

namespace Archysoft.Domain.Model.Extensions
{
    public static class FilterExtensions
    {
        public static SearchResult<T> BaseFilter<T>(this IQueryable<T> query, BaseFilter filter)
        {
            var totalCount = query.Count();

            // Ordering
            if (!string.IsNullOrEmpty(filter.OrderBy))
            {
                query = query.OrderBy(filter.OrderBy);
            }

            // Pagination
            if (filter.PageIndex.HasValue)
                query = query.Skip(filter.PageIndex.Value * filter.PageSize ?? 10);

            if (filter.PageSize.HasValue)
                query = query.Take(filter.PageSize.Value);

            return new SearchResult<T>
            {
                Data = query.ToList(),
                Total = totalCount
            };
        }

        public static IQueryable<User> FilterUsers(this IQueryable<User> query, BaseFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = query.Where(x => x.UserName.Contains(filter.GetTrimSearch()) || x.Email.Contains(filter.GetTrimSearch()));
            }

            return query;
        }
    }
}
