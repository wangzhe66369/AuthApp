using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace VCrisp.PageHelper
{

	public class PageResult
	{
		/// <summary>
		/// 当前页
		/// </summary>
		public int PageIndex { get; set; }

		/// <summary>
		/// 总记录数
		/// </summary>
		public int TotalRecordsCount { get; set; }

		/// <summary>
		/// 总页数
		/// </summary>
		public int TotalPagesCount { get; set; }

		/// <summary>
		/// 每页记录数
		/// </summary>
		public int? PageSize { get; set; }
		/// <summary>
		/// 数据集
		/// </summary>
		public object Records { get; set; }
	}

	/// <summary>
	/// 数据分页信息
	/// </summary>
	public class PageResult<T>
	{

		public PageResult<TResult> ToPageResult<TResult>(Func<IEnumerable<T>, IEnumerable<TResult>> func)
		{
			return new PageResult<TResult>(func(Records), this.PageIndex, this.PageSize, this.TotalRecordsCount, this.TotalPagesCount);
		}

		public PageResult(IEnumerable<T> rows,int? pageIndex, int? pageSize, int totalRecordsCount, int totalPagesCount)
		{
			this.Records = rows;
			this.PageIndex = pageIndex;
			this.PageSize = pageSize;
			this.TotalRecordsCount = totalRecordsCount;
			this.TotalPagesCount = totalPagesCount;
		}

		//public PageResult()
		//: this(new T[0], 0)
		//{ }

		public PageResult(IEnumerable<T> rows, int total)
		{
			this.Records = rows;
			this.TotalRecordsCount = total;
		}

		/// <summary>
		/// 当前页
		/// </summary>
		public int? PageIndex { get; set; }

		/// <summary>
		/// 总记录数
		/// </summary>
		public int TotalRecordsCount { get; set; }

		/// <summary>
		/// 总页数
		/// </summary>
		public int TotalPagesCount { get; set; }

		/// <summary>
		/// 每页记录数
		/// </summary>
		public int? PageSize { get; set; }
		/// <summary>
		/// 数据集
		/// </summary>
		public IEnumerable<T> Records { get; set; }
    }
}
