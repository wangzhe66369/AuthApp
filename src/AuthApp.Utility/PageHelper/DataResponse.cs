using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AuthApp.Utility.PageHelper
{
	/// <summary>
	/// JqGrid输出对象
	/// </summary>
	public class DataResponse
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
		public int PageSize { get; set; }

		/// <summary>
		/// 数据集
		/// </summary>
		public List<ResultRecord> Records { get; set; }

		public DataResponse()
		{
			Records = new List<ResultRecord>();
		}

		public object ToJsonResult()
		{
			TotalPagesCount = (int)Math.Ceiling((float)TotalRecordsCount / (float)PageSize);
			object[] array = new object[Records.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new
				{
					id = Records[i].Id,
					cell = Records[i].List.ToArray()
				};
			}
			//string jsonString = JsonSerializer.Serialize(new
			//{
			//	page = PageIndex,
			//	records = TotalRecordsCount,
			//	total = TotalPagesCount,
			//	rows = array
			//});
			return new
			{
				page = this.PageIndex,
				records = this.TotalRecordsCount,
				total = this.TotalPagesCount,
				rows = array
			};
		}
	}


	/// <summary>
	/// JqGrid输出对象
	/// </summary>
	public class DataResponse<T>
	{
		public DataResponse()
		{
			this.Records = new List<ResultRecord<T>>();
		}

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
		public int PageSize { get; set; }

		/// <summary>
		/// 数据集
		/// </summary>
		public List<ResultRecord<T>> Records { get; set; }

		public object ToJsonResult()
		{
			this.TotalPagesCount = (int)Math.Ceiling((double)((float)this.TotalRecordsCount / (float)this.PageSize));
			object[] array = new object[this.Records.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new
				{
					id = this.Records[i].Id,
					cell = this.Records[i].Entity
				};
			}
			//string jsonString=JsonSerializer.Serialize(new
			//{
			//	page = this.PageIndex,
			//	records = this.TotalRecordsCount,
			//	total = this.TotalPagesCount,
			//	rows = array
			//});

			return new
			{
				page = this.PageIndex,
				records = this.TotalRecordsCount,
				total = this.TotalPagesCount,
				rows = array
			};
			//return jsonString;
		}
	}
}
