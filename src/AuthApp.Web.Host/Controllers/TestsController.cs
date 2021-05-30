using AuthApp.Utility.Entity;
using AuthApp.Utility.PageHelper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthApp.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        // GET: api/<TestsController>
        //[HttpGet]
        //public object Get()
        //{
        //    //string sord, int page, int rows, string sidx
        //    string sord = "";
        //    int page = 1;
        //    int rows = 10;
        //    string sidx = "";
        //    var jqGridResponse = new DataResponse<MaterialTypeDto> { PageIndex = page, PageSize = rows};

        //    //IQueryable<MaterialType> data = MaterialTypeToResult.QueryList(materialTypeCondition).Where(d => d.Stationcode == AppContext.CurrentUser.ProjectCode).OrderBy("CreateDate", SortDirection.Descending);
        //    IQueryable<MaterialType> data = MaterialTypeToResult.QueryList().AsQueryable();


        //    var pagedViewModel = new PagedViewModel<MaterialType>
        //    {
        //        Query = data,
        //        SortOptions = new SortOptions() { Column = sidx, Direction = sord.Equals("asc") ? SortDirection.Ascending : SortDirection.Descending },
        //        DefaultSortColumn = sidx,
        //        Page = page,
        //        PageSize = rows,
        //    }
        //    .Setup();

        //    jqGridResponse.TotalRecordsCount = pagedViewModel.PagedList.TotalItems;

        //    pagedViewModel.PagedList.ToList().ForEach(d =>
        //    {
        //        jqGridResponse.Records.Add(new ResultRecord<MaterialTypeDto>()
        //        {
        //            Id = d.Id,
        //            Entity = new MaterialTypeDto(){
        //            Name = d.Name,
        //            Age=d.Age,
        //            }
        //        });
        //    });
        //    return jqGridResponse;
        //}
        [HttpGet]
        public object Get()
        {
            //string sord, int page, int rows, string sidx
            string sord = "";
            int page = 1;
            int rows = 10;
            string sidx = "";
            var jqGridResponse = new DataResponse<MaterialTypeDto> { PageIndex = page, PageSize = rows };

            IQueryable<MaterialType> data = MaterialTypeToResult.QueryList().AsQueryable();

            var pagedViewModel = new PagedViewModel<MaterialType>
            {
                Query = data,
                SortOptions = new SortOptions() { Column = sidx, Direction = sord.Equals("asc") ? SortDirection.Ascending : SortDirection.Descending },
                DefaultSortColumn = sidx,
                Page = page,
                PageSize = rows,
            }
            .Setup();

            jqGridResponse.TotalRecordsCount = pagedViewModel.PagedList.TotalItems;

            pagedViewModel.PagedList.ToList().ForEach(d =>
            {
                jqGridResponse.Records.Add(new ResultRecord<MaterialTypeDto>()
                {
                    Id = d.Id,
                    Entity = new MaterialTypeDto()
                    {
                        Name = d.Name,
                        Age = d.Age,
                    }
                });
            });
            return jqGridResponse;
        }
    }
}
