using System;
using System.Collections.Generic;
using System.Text;

namespace VCrisp.PageHelper
{
	public class SortOptions
	{
		public string Column { get; set; }

		public SortDirection Direction { get; set; }
	}

	public enum SortDirection
	{
		Ascending,
		Descending
	}
}
