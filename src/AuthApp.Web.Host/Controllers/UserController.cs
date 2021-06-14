using AuthApp.Identity.Users;
using AuthApp.Users.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserController(
            UserManager<User> userManager,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// 读取用户列表信息
        /// </summary>
        /// <returns>用户列表信息</returns>
        [HttpPost]
        //[ModuleInfo]
        //[Description("读取")]
        [HttpPost]
        public  PageResult<UserOutputDto> Read(PageRequest request)
        {
            var pagedViewModel = new PagedViewModel<User>
            {
                Query = _userManager.Users,
                SortOptions = new SortOptions() { Column = request.PageCondition.Sidx, Direction = request.PageCondition.Sord.Equals("asc") ? SortDirection.Ascending : SortDirection.Descending },
                DefaultSortColumn = request.PageCondition.Sidx,
                Page = request.PageCondition.PageIndex,
                PageSize = request.PageCondition.PageSize,
            }.Setup();


            PageResult<UserOutputDto> pageResult = pagedViewModel.ToPageData(u => new UserOutputDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                EmailConfirmed = u.EmailConfirmed,
                PhoneNumber = u.PhoneNumber,
                PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                LockoutEnd = u.LockoutEnd,
                LockoutEnabled = u.LockoutEnabled,
                AccessFailedCount = u.AccessFailedCount,
                //Roles = _userManager.GetRolesAsync(u).ro
            });

            return pageResult;
        }

        ///// <summary>
        ///// 读取用户节点信息
        ///// </summary>
        ///// <param name="group"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ListNode[] ReadNode(FilterGroup group)
        //{
        //    Check.NotNull(group, nameof(group));
        //    IFunction function = this.GetExecuteFunction();
        //    Expression<Func<User, bool>> exp = _filterService.GetExpression<User>(group);
        //    ListNode[] nodes = _cacheService.ToCacheArray<User, ListNode>(_userManager.Users, exp, m => new ListNode()
        //    {
        //        Id = m.Id,
        //        Name = m.NickName
        //    }, function);
        //    return nodes;
        //}

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="dtos">用户信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        //[ModuleInfo]
        //[DependOnFunction("Read")]
        //[UnitOfWork]
        //[Description("新增")]
        public async Task<AjaxResult> Create(UserInputDto[] dtos)
        {
            List<string> names = new List<string>();
            foreach (var dto in dtos)
            {
                User user = _mapper.Map<User>(dto);
                IdentityResult result = dto.Password.IsNullOrWhiteSpace()
                    ? await _userManager.CreateAsync(user)
                    : await _userManager.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    return new AjaxResult(result.Errors.Select((IdentityError err) => err.Description).JoinAsString(), AjaxResultType.Error);
                }
                names.Add(user.UserName);
            }
            return new AjaxResult($"用户“{ names.JoinAsString()}”创建成功");
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="dtos">用户信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
        //[ModuleInfo]
        //[DependOnFunction("Read")]
        //[UnitOfWork]
        //[Description("更新")]
        public async Task<AjaxResult> Update(UserInputDto[] dtos)
        {
            //Check.NotNull(dtos, nameof(dtos));
            List<string> names = new List<string>();
            foreach (var dto in dtos)
            {
                User user = await _userManager.FindByIdAsync(dto.Id.ToString());
                user = _mapper.Map<UserInputDto,User>(dto);
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new AjaxResult(result.Errors.Select((IdentityError err) => err.Description).JoinAsString(), AjaxResultType.Error);
                }
                names.Add(user.UserName);
            }
            return new AjaxResult($"用户“{names.JoinAsString()}”更新成功");
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="ids">用户信息</param>
        /// <returns>JSON操作结果</returns>
        [HttpPost]
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
                User user = await _userManager.FindByIdAsync(id.ToString());
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return new AjaxResult(result.Errors.Select((IdentityError err) => err.Description).JoinAsString(), AjaxResultType.Error);
                    //return result.ToOperationResult().ToAjaxResult();
                }
                names.Add(user.UserName);
            }
            return new AjaxResult($"用户“{names.JoinAsString()}”删除成功");
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="dto">用户角色信息</param>
        /// <returns>JSON操作结果</returns>
        //[HttpPost]
        //[ModuleInfo]
        //[DependOnFunction("Read")]
        //[DependOnFunction("ReadUserRoles", Controller = "Role")]
        //[UnitOfWork]
        //[Description("设置角色")]
        //public async Task<AjaxResult> SetRoles(UserSetRoleDto dto)
        //{
        //    OperationResult result = await _identityContract.SetUserRoles(dto.UserId, dto.RoleIds);
        //    return result.ToAjaxResult();
        //}

        /// <summary>
        /// 设置用户模块
        /// </summary>
        /// <param name="dto">用户模块信息</param>
        /// <returns>JSON操作结果</returns>
        //[HttpPost]
        //[ModuleInfo]
        //[DependOnFunction("Read")]
        //[DependOnFunction("ReadUserModules", Controller = "Module")]
        //[UnitOfWork]
        //[Description("设置模块")]
        //public async Task<AjaxResult> SetModules(UserSetModuleDto dto)
        //{
        //    OperationResult result = await _functionAuthManager.SetUserModules(dto.UserId, dto.ModuleIds);
        //    return result.ToAjaxResult();
        //}
    }
}