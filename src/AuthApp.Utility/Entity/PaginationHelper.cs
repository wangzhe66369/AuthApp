using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthApp.Utility.Entity
{
	public static class PaginationHelper
	{
		public static IPagination<T> AsPagination<T>(this IEnumerable<T> source, int pageNumber)
		{
			return source.AsPagination(pageNumber, 20);
		}
		public static IPagination<T> AsPagination<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
		{
			if (pageNumber < 1)
			{
				throw new ArgumentOutOfRangeException("pageNumber", "The page number should be greater than or equal to 1.");
			}
			return new LazyPagination<T>(source.AsQueryable<T>(), pageNumber, pageSize);
		}
	}
}
