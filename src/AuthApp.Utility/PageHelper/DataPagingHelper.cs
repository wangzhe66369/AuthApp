using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using VCrisp.Utilities.Filter;

namespace VCrisp.Utilities.PageHelper
{
	/// <summary>
	/// 分页排序助手类
	/// </summary>
	public static class DataPagingHelper
	{
		public static IQueryable<T> GetQueryable<T>(this IList<T> list, string sidx, string sord, int page, int rows)
		{
			return list.AsQueryable<T>().GetQueryable(sidx, sord, page, rows);
		}

		public static IQueryable<T> GetQueryable<T>(this IQueryable<T> queriable, string sidx, string sord, int page, int rows)
		{
			IOrderedQueryable<T> source = DataPagingHelper.ApplyOrder<T>(queriable, sidx, sord.ToLower() == "asc");
			return source.Skip((page - 1) * rows).Take(rows);
		}

		private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> queriable, string property, bool isASC)
		{
			PropertyInfo property2 = typeof(T).GetProperty(property);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "x");
			Expression body = Expression.Property(parameterExpression, property2);
			Type delegateType = typeof(Func<,>).MakeGenericType(new Type[]
			{
				typeof(T),
				property2.PropertyType
			});
			LambdaExpression lambdaExpression = Expression.Lambda(delegateType, body, new ParameterExpression[]
			{
				parameterExpression
			});
			string methodName = isASC ? "OrderBy" : "OrderByDescending";
			object obj = typeof(Queryable).GetMethods().Single(
				(MethodInfo method) => method.Name == methodName && method.IsGenericMethodDefinition && method.GetGenericArguments().Length == 2 && method.GetParameters().Length == 2
			).MakeGenericMethod(new Type[]
			{
				typeof(T),
				property2.PropertyType
			}).Invoke(null, new object[]
			{
				queriable,
				lambdaExpression
			});
			return (IOrderedQueryable<T>)obj;
		}

		public static PageResult<TResult> ToPage<TEntity, TResult>(this IQueryable<TEntity> source,
		   Expression<Func<TEntity, bool>> predicate,
		   PageCondition pageCondition,
		   Expression<Func<TEntity, TResult>> selector)
		{
			//source.CheckNotNull("source");
			//predicate.CheckNotNull("predicate");
			//pageCondition.CheckNotNull("pageCondition");
			//selector.CheckNotNull("selector");

			return source.ToPage(predicate, pageCondition.Sidx, pageCondition.Sord, pageCondition.PageIndex, pageCondition.PageSize,selector);
		}

		public static PageResult<TResult> ToPage<TEntity, TResult>(this IQueryable<TEntity> source,
			Expression<Func<TEntity, bool>> predicate,
			string sidx, 
			string sord,
			int pageIndex,
			int pageSize,
			
			Expression<Func<TEntity, TResult>> selector)
		{
			//source.CheckNotNull("source");
			//predicate.CheckNotNull("predicate");
			//pageIndex.CheckGreaterThan("pageIndex", 0);
			//pageSize.CheckGreaterThan("pageSize", 0);
			//selector.CheckNotNull("selector");

			var total = source.Count(predicate);
			TResult[] data = source.Where(predicate).GetQueryable(sidx, sord, pageIndex, pageSize).Select(selector).ToArray();
			return new PageResult<TResult>(data, total) ;
		}
	}
}
