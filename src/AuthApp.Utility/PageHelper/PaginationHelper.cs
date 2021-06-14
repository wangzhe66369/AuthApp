using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace VCrisp.Utilities.PageHelper
{
	public static class PaginationHelper
	{
		public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> datasource, string propertyName, SortDirection direction)
		{
			return datasource.AsQueryable<T>().OrderBy(propertyName, direction);
		}

		public static IQueryable<T> OrderBy<T>(this IQueryable<T> datasource, string propertyName, SortDirection direction)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				return datasource;
			}
			Type typeFromHandle = typeof(T);
			PropertyInfo property = typeFromHandle.GetProperty(propertyName);
			if (property == null)
			{
				throw new InvalidOperationException(string.Format("Could not find a property called '{0}' on type {1}", propertyName, typeFromHandle));
			}
			ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, "p");
			MemberExpression body = Expression.MakeMemberAccess(parameterExpression, property);
			LambdaExpression expression = Expression.Lambda(body, new ParameterExpression[]
			{
				parameterExpression
			});
			string methodName = (direction == SortDirection.Ascending) ? "OrderBy" : "OrderByDescending";
			MethodCallExpression expression2 = Expression.Call(typeof(Queryable), methodName, new Type[]
			{
				typeFromHandle,
				property.PropertyType
			}, new Expression[]
			{
				datasource.Expression,
				Expression.Quote(expression)
			});
			return datasource.Provider.CreateQuery<T>(expression2);
		}

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
