using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace VCrisp.Utilities.PageHelper
{
	/// <summary>
	/// 分页ViewModel
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PagedViewModel<T>
	{
		public IQueryable<T> Query { get; set; }

		public SortOptions SortOptions { get; set; }

		public string DefaultSortColumn { get; set; }

		public IPagination<T> PagedList { get; set; }

		public int? Page { get; set; }

		public int? PageSize { get; set; }

		/// <summary>
		/// 当前页的起始索引
		/// </summary>
		public int StartIndex
		{
			get
			{
				return this.PageSize.Value * (this.Page.Value - 1);
			}
		}

		public PagedViewModel<T> AddFilter(Expression<Func<T, bool>> predicate)
		{
			this.Query = this.Query.Where(predicate);
			return this;
		}

		public PagedViewModel<T> AddFilter<TValue>(TValue value, Expression<Func<T, bool>> predicate)
		{
			this.ProcessQuery<TValue>(value, predicate);
			return this;
		}

		public PagedViewModel<T> Setup()
		{
			if (string.IsNullOrWhiteSpace(this.SortOptions.Column))
			{
				this.SortOptions.Column = this.DefaultSortColumn;
			}
			this.PagedList = this.Query.OrderBy(this.SortOptions.Column, this.SortOptions.Direction).AsPagination(this.Page ?? 1, this.PageSize ?? 10);
			return this;
		}

		private void ProcessQuery<TValue>(TValue value, Expression<Func<T, bool>> predicate)
		{
			if (value == null)
			{
				return;
			}
			if (typeof(TValue) == typeof(string) && string.IsNullOrWhiteSpace(value as string))
			{
				return;
			}
			this.Query = this.Query.Where(predicate);
		}


		public PageResult<TValue> ToPageData<TValue>(Func<T,TValue> p)
        {
			var totalRecordsCount = this.PagedList.TotalItems;
			var totalPagesCount = (int)Math.Ceiling((double)((float)totalRecordsCount / (float)this.PageSize));
			IEnumerable <TValue> data = this.PagedList.Select(p);
			return new PageResult<TValue>(data, this.Page, totalRecordsCount, totalPagesCount);
		}
       
    }
}
