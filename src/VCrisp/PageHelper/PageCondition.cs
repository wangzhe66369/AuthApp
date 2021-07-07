using System;
using System.Collections.Generic;
using System.Text;

namespace VCrisp.PageHelper
{
    public class PageCondition
    {
        /// <summary>
        /// 初始化一个 默认参数（第1页，每页20，排序条件为空）的分页查询条件信息类 的新实例
        /// </summary>
        public PageCondition()
            //: this(1, 10)
        { }

        /// <summary>
        /// 初始化一个 指定页索引与页大小的分页查询条件信息类 的新实例
        /// </summary>
        /// <param name="pageIndex"> 页索引 </param>
        /// <param name="pageSize"> 页大小 </param>
        //public PageCondition(int pageIndex, int pageSize)
        //{
        //    //pageIndex.CheckGreaterThan("pageIndex", 0);
        //    //pageSize.CheckGreaterThan("pageSize", 0);
        //    PageIndex = pageIndex;
        //    PageSize = pageSize;
        //    //SortConditions = new SortCondition[] { };
        //}

        /// <summary>
        /// 获取或设置 页索引
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 获取或设置 页大小
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 排序顺序正序或者逆序
        /// </summary>
        public string Sord { get; set; } = "ascending";

        /// <summary>
        /// 排序列
        /// </summary>
        public string Sidx { get; set; } = "Id";



        /// <summary>
        /// 获取或设置 排序条件组
        /// </summary>
        //public SortCondition[] SortConditions { get; set; }
    }
}