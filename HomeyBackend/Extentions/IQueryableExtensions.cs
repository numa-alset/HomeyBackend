using AutoMapper.QueryableExtensions;
using HomeyBackend.Core.Models;
using Microsoft.OpenApi.Extensions;
using System.Linq.Expressions;

namespace HomeyBackend.Extentions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryableObjects queryObj, Dictionary<SortBy, Expression<Func<T, object>>> columnsMap)
        {
            if (queryObj.SortBy==null || !columnsMap.ContainsKey(queryObj.SortBy.Value))
                return query;

            if (queryObj.IsSortAscending)
                return query.OrderBy(columnsMap[queryObj.SortBy.Value]);
            else
                return query.OrderByDescending(columnsMap[queryObj.SortBy.Value]);
        }
        public static IQueryable<T> ApllyPaging<T>(this IQueryable<T> query, IQueryableObjects queryObj)
        {
            if (queryObj.Page <= 0)
                queryObj.Page= 0;
            return query.Skip(queryObj.Page ).Take(10);
        }
        

    }
}
