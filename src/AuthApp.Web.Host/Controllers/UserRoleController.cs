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
    public class UserRoleController : ControllerBase
    {
        ///// <summary>
        ///// 读取用户角色信息
        ///// </summary>
        ///// <returns>JSON操作结果</returns>
        //[HttpPost]
        ////[ModuleInfo]
        ////[Description("读取")]
        //public PageData<UserRoleOutputDto> Read(PageRequest request)
        //{
        //    Expression<Func<IdentityUserRole, bool>> predicate = _filterService.GetExpression<UserRole>(request.FilterGroup);
        //    Func<UserRole, bool> updateFunc = _filterService.GetDataFilterExpression<UserRole>(null, DataAuthOperation.Update).Compile();
        //    Func<UserRole, bool> deleteFunc = _filterService.GetDataFilterExpression<UserRole>(null, DataAuthOperation.Delete).Compile();

        //    PageResult<UserRoleOutputDto> page = _identityContract.UserRoles.ToPage(predicate, request.PageCondition, m => new
        //    {
        //        D = m,
        //        UserName = m.User.UserName,
        //        RoleName = m.Role.Name,
        //    }).ToPageResult(data => data.Select(m => new UserRoleOutputDto(m.D)
        //    {
        //        UserName = m.UserName,
        //        RoleName = m.RoleName,
        //        Updatable = updateFunc(m.D),
        //        Deletable = deleteFunc(m.D)
        //    }).ToArray());
        //    return page.ToPageData();
        //}

        ///// <summary>
        ///// 更新用户角色信息
        ///// </summary>
        ///// <param name="dtos">用户角色信息</param>
        ///// <returns>JSON操作结果</returns>
        //[HttpPost]
        //[ModuleInfo]
        //[DependOnFunction("Read")]
        //[UnitOfWork]
        //[Description("更新")]
        //public async Task<AjaxResult> Update(UserRoleInputDto[] dtos)
        //{
        //    Check.NotNull(dtos, nameof(dtos));

        //    OperationResult result = await _identityContract.UpdateUserRoles(dtos);
        //    return result.ToAjaxResult();
        //}

        ///// <summary>
        ///// 删除用户角色信息
        ///// </summary>
        ///// <param name="ids">要删除的用户角色编号</param>
        ///// <returns>JSON操作结果</returns>
        //[HttpPost]
        //[ModuleInfo]
        //[DependOnFunction("Read")]
        //[UnitOfWork]
        //[Description("删除")]
        //public async Task<AjaxResult> Delete(Guid[] ids)
        //{
        //    Check.NotNull(ids, nameof(ids));

        //    OperationResult result = await _identityContract.DeleteUserRoles(ids);
        //    return result.ToAjaxResult();
        //}

    }
}