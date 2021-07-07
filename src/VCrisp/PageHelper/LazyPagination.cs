using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCrisp.PageHelper
{
	public class LazyPagination<T> : IPagination<T>, IPagination, IEnumerable<T>, IEnumerable
	{
		public int PageSize { get; private set; }
		public IQueryable<T> Query { get; protected set; }
		public int PageNumber { get; private set; }
		public LazyPagination(IQueryable<T> query, int pageNumber, int pageSize)
		{
			this.PageNumber = pageNumber;
			this.PageSize = pageSize;
			this.Query = query;
		}
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			this.TryExecuteQuery();
			foreach (T item in this.results)
			{
				yield return item;
			}
			yield break;
		}
		protected void TryExecuteQuery()
		{
			if (this.results != null)
			{
				return;
			}
			this.totalItems = this.Query.Count<T>();
			this.results = this.ExecuteQuery();
		}
		protected virtual IList<T> ExecuteQuery()
		{
			int count = (this.PageNumber - 1) * this.PageSize;
			return this.Query.Skip(count).Take(this.PageSize).ToList<T>();
		}
		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}
		public int TotalItems
		{
			get
			{
				this.TryExecuteQuery();
				return this.totalItems;
			}
		}
		public int TotalPages
		{
			get
			{
				return (int)Math.Ceiling((double)this.TotalItems / (double)this.PageSize);
			}
		}
		public int FirstItem
		{
			get
			{
				this.TryExecuteQuery();
				return (this.PageNumber - 1) * this.PageSize + 1;
			}
		}
		public int LastItem
		{
			get
			{
				return this.FirstItem + this.results.Count - 1;
			}
		}
		public bool HasPreviousPage
		{
			get
			{
				return this.PageNumber > 1;
			}
		}
		public bool HasNextPage
		{
			get
			{
				return this.PageNumber < this.TotalPages;
			}
		}
		public const int DefaultPageSize = 20;
		private IList<T> results;
		private int totalItems;
	}
}
