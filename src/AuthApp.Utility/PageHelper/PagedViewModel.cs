using AuthApp.Utility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AuthApp.Utility.PageHelper
{
	/// <summary>
	/// 分页ViewModel
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PagedViewModel<T>
	{
		//public ViewDataDictionary ViewData { get; set; }

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

		public PagedViewModel<T> AddFilter<TValue>(string key, TValue value, Expression<Func<T, bool>> predicate)
		{
			this.ProcessQuery<TValue>(value, predicate);
			//this.ViewData[key] = value;
			return this;
		}

		public PagedViewModel<T> AddFilter<TValue>(string keyField, object value, Expression<Func<T, bool>> predicate, IQueryable<TValue> query, string textField)
		{
			this.ProcessQuery<object>(value, predicate);
			//SelectList selectList = new SelectList(query, keyField, textField, value ?? -1);
			//this.ViewData[keyField] = selectList;
			//SelectListItem kk = new SelectListItem();
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
	}
}
