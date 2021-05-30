using System;
using System.Collections.Generic;
using System.Text;

namespace AuthApp.Utility.PageHelper
{
	public class ResultRecord
	{
		public object Id { get; set; }
		public List<object> List { get; set; }
		public ResultRecord()
		{
		}
        public ResultRecord(string id, List<object> list)
        {
            Id = id;
            List = list;
        }
    }
	public class ResultRecord<T>
	{
		public object Id { get; set; }
		public T Entity { get; set; }
		public ResultRecord()
		{
		}
		public ResultRecord(string id, T entity)
		{
			this.Id = id;
			this.Entity = entity;
		}
	}
}
