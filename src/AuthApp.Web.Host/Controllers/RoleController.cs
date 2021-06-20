using AuthApp.Identity.Roles;
using AuthApp.Roles;
using AuthApp.Roles.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCrisp.Data;
using VCrisp.Extensions.BaseType;
using VCrisp.UI;
using VCrisp.Utilities.Filter;
using VCrisp.Utilities.PageHelper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthApp.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly IRoleService _iRoleService;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public RoleController(IRoleService iRoleService, IMapper mapper)
        {
            _iRoleService = iRoleService;
            _mapper = mapper;
        }

        /// <summary>
        /// 读取角色
        /// </summary>
        /// <returns>角色页列表</returns>
        [HttpPost("ReadRole", Name = nameof(Read))]
        //[ModuleInfo]
        //[Description("读取")]
        public PageResult<RoleOutputDto> Read(PageRequest request)
        {

            var pagedViewModel = new PagedViewModel<Role>
            {
                Query = _roleManager.Roles,
                SortOptions = new SortOptions() { Column = request.PageCondition.Sidx, Direction = request.PageCondition.Sord.Equals("asc") ? SortDirection.Ascending : SortDirection.Descending },
                DefaultSortColumn = request.PageCondition.Sidx,
                Page = request.PageCondition.PageIndex,
                PageSize = request.PageCondition.PageSize,
            }.Setup();


            PageResult<RoleOutputDto> pageResult = pagedViewModel.ToPageData(r => new RoleOutputDto
            {
                Id = r.Id,
                Name = r.Name,
                //NormalizedName = u.NormalizedName
            });

            return pageResult;
        }

        /// <summary>
        /// 读取角色节点
        /// </summary>
        /// <returns>角色节点列表</returns>
        //[HttpGet]
        //[ModuleInfo]
        //[Description("读取节点")]
        //public RoleNode[] ReadNode()
        //{
        //    IFunction function = this.GetExecuteFunction();
        //    Expression<Func<Role, bool>> exp = m => !m.IsLocked;

        //    RoleNode[] nodes = _cacheService.ToCacheArray(_roleManager.Roles, exp, m => new RoleNode()
        //    {
        //        RoleId = m.Id,
        //        RoleName = m.Name
        //    }, function);
        //    return nodes;
        //}

        ///// <summary>
        ///// 读取角色[用户]树数据
        ///// </summary>
        ///// <param name="userId">用户编号</param>
        ///// <returns>角色[用户]树数据</returns>
        //[HttpGet]
        //[Description("读取角色[用户]树数据")]
        //public List<UserRoleNode> ReadUserRoles(int userId)
        //{
        //    Check.GreaterThan(userId, nameof(userId), 0);

        //    int[] checkRoleIds = _identityContract.UserRoles.Where(m => m.UserId == userId).Select(m => m.RoleId).Distinct().ToArray();
        //    List<UserRoleNode> nodes = _identityContract.Roles.Where(m => !m.IsLocked)
        //        .OrderByDescending(m => m.IsAdmin).ThenBy(m => m.Id).ToOutput<Role, UserRoleNode>().ToList();
        //    nodes.ForEach(m => m.IsChecked = checkRoleIds.Contains(m.Id));
        //    return nodes;
        //}

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="dtos">新增角色信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost("CreateRole", Name = nameof(Create))]
        //[ModuleInfo]
        //[DependOnFunction("Read")]
        //[UnitOfWork]
        //[Description("新增")]
        public async Task<AjaxResult> Create(RoleInputDto[] dtos)
        {
            //Check.NotNull(dtos, nameof(dtos));
            List<string> names = new List<string>();
            foreach (RoleInputDto dto in dtos)
            {
                Role role = _mapper.Map<Role>(dto);
                IdentityResult result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    return new AjaxResult(result.Errors.Select((IdentityError err) => err.Description).JoinAsString(), AjaxResultType.Error);
                }
                names.Add(role.Name);
            }
            return new AjaxResult($"角色“{names.JoinAsString()}”创建成功");
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="dtos">更新角色信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost("UpdateRole", Name = nameof(Update))]
        //[ModuleInfo]
        //[DependOnFunction("Read")]
        //[UnitOfWork]
        //[Description("更新")]
        public async Task<AjaxResult> Update(RoleInputDto[] dtos)
        {
            //Check.NotNull(dtos, nameof(dtos));
            List<string> names = new List<string>();
            foreach (RoleInputDto dto in dtos)
            {
                Role role = await _roleManager.FindByIdAsync(dto.Id.ToString());

                role = _mapper.Map<Role>(dto);
                IdentityResult result = await _roleManager.UpdateAsync(role);
                if (!result.Succeeded)
                {
                    return new AjaxResult(result.Errors.Select((IdentityError err) => err.Description).JoinAsString(), AjaxResultType.Error);
                }
                names.Add(role.Name);
            }
            return new AjaxResult($"角色“{names.JoinAsString()}”更新成功");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids">要删除的角色编号</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost("DeleteRole", Name = nameof(Delete))]
        //[ModuleInfo]
        //[DependOnFunction("Read")]
        //[UnitOfWork]
        //[Description("删除")]
        public async Task<AjaxResult> Delete(int[] ids)
        {
            //Check.NotNull(ids, nameof(ids));
            List<string> names = new List<string>();
            foreach (int id in ids)
            {
                Role role = await _roleManager.FindByIdAsync(id.ToString());
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    return new AjaxResult(result.Errors.Select((IdentityError err) => err.Description).JoinAsString(), AjaxResultType.Error);
                }
                names.Add(role.Name);
            }
            return new AjaxResult($"角色“{names.JoinAsString()}”删除成功");
        }

        ///// <summary>
        ///// 设置角色模块
        ///// </summary>
        ///// <param name="dto">角色模块信息</param>
        ///// <returns>JSON操作结果</returns>
        //[HttpPost]
        //[ModuleInfo]
        //[DependOnFunction("Read")]
        //[DependOnFunction("ReadRoleModules", Controller = "Module")]
        //[UnitOfWork]
        //[Description("设置模块")]
        //public async Task<AjaxResult> SetModules(RoleSetModuleDto dto)
        //{
        //    OperationResult result = await _functionAuthorizationManager.SetRoleModules(dto.RoleId, dto.ModuleIds);
        //    return result.ToAjaxResult();
        //}
    }
}
