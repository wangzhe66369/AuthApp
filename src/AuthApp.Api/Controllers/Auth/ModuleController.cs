using AuthApp.Api.HttpContextUser;
using AuthApp.Domian.Auth;
using AuthApp.Domian.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VCrisp;
using VCrisp.PageHelper;
using VCrisp.UI;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthApp.Api.Controllers.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {

        private readonly IModulePermissionRepository _modulePermissionRepository;
        private readonly IAspNetUser _aspNetUser;


        public ModuleController(IModulePermissionRepository modulePermissionRepository, IAspNetUser aspNetUser)
        {
            _modulePermissionRepository = modulePermissionRepository;
            _aspNetUser = aspNetUser;
        }
        /// <summary>
        /// 获取全部接口api
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        // GET: api/User
        [HttpGet]
        public async Task<MessageModel<PageResult<ModulePermission>>> Get(int page = 1,  string key = "")
        {
            
            PageCondition pageCondition = new PageCondition()
            {
                Sidx = "Id",
                Sord= "asc",
            };

            Expression<Func<ModulePermission, bool>> whereExpression = a => a.IsDeleted != true; //&& (a.Name != null && a.Name.Contains(key));
            var query = await _modulePermissionRepository.GetAllAsync();
            var pagedViewModel = new PagedViewModel<ModulePermission>
            {
                Query = query,
                SortOptions = new SortOptions() { Column = pageCondition.Sidx, Direction = pageCondition.Sord.Equals("asc") ? SortDirection.Ascending : SortDirection.Descending },
                DefaultSortColumn = pageCondition.Sidx,
                Page = pageCondition.PageIndex,
                PageSize = pageCondition.PageSize,
            }.AddFilter(whereExpression)
           .Setup();

            PageResult<ModulePermission> pageResult = pagedViewModel.ToPageData();

            return new MessageModel<PageResult<ModulePermission>>()
            {
                msg = "获取成功",
                success = pageResult.TotalRecordsCount >= 0,
                response = pageResult
            };

        }


        /// <summary>
        /// 添加一条接口信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        // POST: api/User
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] ModulePermission module)
        {
            var data = new MessageModel<string>();

            module.CreateId = _aspNetUser.ID;
            module.CreateBy = _aspNetUser.Name;

            var addModule = (await _modulePermissionRepository.AddAsync(module));
            if (addModule!=null)
            {
                data.success = true;
                data.response = addModule.Id.ObjToString();
                data.msg = "添加成功";
            }

            return data;
        }

        /// <summary>
        /// 更新接口信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] ModulePermission module)
        {
            var data = new MessageModel<string>();
            if (module != null && module.Id > 0)
            {
                var updatedModule = await _modulePermissionRepository.UpdateAsync(module);
                if (updatedModule!=null)
                {
                    data.success = true;
                    data.msg = "更新成功";
                    data.response = updatedModule?.Id.ObjToString();
                }
            }

            return data;
        }

        /// <summary>
        /// 删除一条接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var userDetail = await _modulePermissionRepository.GetAsync(id);
                userDetail.IsDeleted = true;
                var updatedModule = await _modulePermissionRepository.UpdateAsync(userDetail);
                if (updatedModule!=null)
                {
                    data.success = true;
                    data.msg = "删除成功";
                    data.response = userDetail?.Id.ObjToString();
                }
            }

            return data;
        }
    }
}
