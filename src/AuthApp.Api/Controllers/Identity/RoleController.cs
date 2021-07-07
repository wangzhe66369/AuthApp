using AuthApp.Domian.Identity;
using AuthApp.Roles;
using AuthApp.Roles.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCrisp.Data;
using VCrisp;
using VCrisp.UI;
using VCrisp.PageHelper;
using AuthApp.Api.HttpContextUser;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthApp.Web.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IAspNetUser _aspNetUser;
        public RoleController(IRoleRepository roleRepository, IMapper mapper, IAspNetUser aspNetUser)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _aspNetUser = aspNetUser;
        }

        /// <summary>
        /// 获取全部角色
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        // GET: api/User
        [HttpGet]
        public async Task<MessageModel<PageResult<RoleOutputDto>>> Get([FromQuery] PageCondition pageCondition )
        {
            //PageCondition pageCondition = new PageCondition()
            //{
            //    Sidx = "Id",
            //    Sord = "asc",
            //};
            //pageCondition.Sidx = "Id";
            //pageCondition.Sord = "ascending";
            var query = await _roleRepository.GetAllAsync();
            var pagedViewModel = new PagedViewModel<Role>
            {
                Query = query,
                SortOptions = new SortOptions() { Column = pageCondition.Sidx, Direction = pageCondition.Sord.Equals("ascending") ? SortDirection.Ascending : SortDirection.Descending },
                DefaultSortColumn = pageCondition.Sidx,
                Page = pageCondition.PageIndex,
                PageSize = pageCondition.PageSize,
            }
            //.AddFilter(a => a.IsDeleted != true) //&& (a.RoleName != null && a.RoleName.Contains(key)))
            .Setup();

            //var data = await _roleRepository.QueryPage(a => a.IsDeleted != true && (a.Name != null && a.Name.Contains(key)), page, intPageSize, " Id desc ");
            PageResult<RoleOutputDto> pageResult = pagedViewModel.ToPageData(p=>new RoleOutputDto {
                Id=p.Id,
                RoleName = p.RoleName,
                Description = p.Description,
                CreateTime = p.CreateTime,
                Enabled = p.Enabled
            });
            return new MessageModel<PageResult<RoleOutputDto>>()
            {
                msg = "获取成功",
                success = pageResult.TotalRecordsCount >= 0,
                response = pageResult
            };

        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        // POST: api/User
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] Role role)
        {
            var data = new MessageModel<string>();

            role.CreateId = _aspNetUser.ID;
            role.CreateBy = _aspNetUser.Name;

            var addRole = (await _roleRepository.AddAsync(role));

            await _roleRepository.UnitOfWork.SaveEntitiesAsync();
            if (addRole!=null)
            {
                data.success = true;
                data.response = addRole.Id.ObjToString();
                data.msg = "添加成功";
            }

            return data;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] Role role)
        {
            var data = new MessageModel<string>();
            if (role != null && role.Id > 0)
            {
                var roleUpdate = await _roleRepository.UpdateAsync(role);
                if (roleUpdate!=null)
                {
                    data.success = true;
                    data.msg = "更新成功";
                    data.response = role?.Id.ObjToString();
                }
            }

            return data;
        }

        /// <summary>
        /// 删除角色
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
                var userDetail = await _roleRepository.GetAsync(id);
                userDetail.IsDeleted = true;
                var roleUpdate = await _roleRepository.UpdateAsync(userDetail);
                if (roleUpdate!=null)
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
