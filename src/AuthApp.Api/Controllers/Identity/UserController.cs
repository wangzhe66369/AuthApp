using AuthApp.Domian.Identity;
using AuthApp.Roles.Dto;
using AuthApp.Users.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCrisp.Data;
using VCrisp.Extensions;
using VCrisp.UI;
using VCrisp.PageHelper;
using Microsoft.AspNetCore.Authorization;
using VCrisp.Utilities.Authorizations;
using VCrisp.Utilities.Helper;
using AuthApp.Api.HttpContextUser;
using VCrisp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthApp.Web.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAspNetUser _aspNetUser;

        public UserController(IMapper mapper,IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IAspNetUser aspNetUser)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _aspNetUser = aspNetUser;
        }

        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        // GET: api/User
        [HttpGet]
        public async Task<MessageModel<PageResult<UserOutputDto>>> Get(int page = 1, string key = "")
        {
           
            PageCondition pageCondition = new PageCondition()
            {
                Sidx = "Id",
                Sord = "asc",
                PageSize=300
            };

            var query =await _userRepository.GetAllAsync();
            var pagedViewModel = new PagedViewModel<User>
            {
                Query = query,
                SortOptions = new SortOptions() { Column = pageCondition.Sidx, Direction = pageCondition.Sord.Equals("asc") ? SortDirection.Ascending : SortDirection.Descending },
                DefaultSortColumn = pageCondition.Sidx,
                Page = pageCondition.PageIndex,
                PageSize = pageCondition.PageSize,
            }.AddFilter(a => a.TdIsDelete != true && a.UserStatus >= 0 )//&& ((a.UserLoginName != null && a.UserLoginName.Contains(key)) || (a.UserRealName != null && a.UserRealName.Contains(key))))
            .Setup();

            // 这里可以封装到多表查询，此处简单处理
            var allUserRoles = await _userRoleRepository.GetByConditionAsync(d => d.IsDeleted == false);
            var allRoles = await _roleRepository.GetByConditionAsync(d => d.IsDeleted == false);

            PageResult<UserOutputDto> pageResult = pagedViewModel.ToPageData( m => new
            {
                D = m,
                RIDs = allUserRoles.Where(d => d.UserId == m.Id).Select(d => d.RoleId).ToList(),
            }).ToPageResult(data => data.Select(m => new UserOutputDto(m.D)
            {
                RIDs = m.RIDs,
                RoleNames = allRoles.Where(d => m.RIDs.Contains(d.Id)).Select(d => d.RoleName).ToList()
            }));

            return new MessageModel<PageResult<UserOutputDto>>()
            {
                msg = "获取成功",
                success = pageResult.TotalRecordsCount >= 0,
                response = pageResult
            };

        }

        // GET: api/User/5
        /// <summary>
        /// 获取用户详情根据token
        /// 【无权限】
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<User>> GetInfoByToken(string token)
        {
            var data = new MessageModel<User>();
            if (!string.IsNullOrEmpty(token))
            {
                var tokenModel = JwtHelper.SerializeJwt(token);
                if (tokenModel != null && tokenModel.Uid > 0)
                {
                    var userinfo =  _userRepository.Get(tokenModel.Uid);
                    if (userinfo != null)
                    {
                        data.response = userinfo;
                        data.success = true;
                        data.msg = "获取成功";
                    }
                }

            }
            return data;
        }

        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="sysUserInfo"></param>
        /// <returns></returns>
        // POST: api/User
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] User sysUserInfo)
        {
            var data = new MessageModel<string>();

            sysUserInfo.UserLoginPWD = MD5Helper.MD5Encrypt32(sysUserInfo.UserLoginPWD);
            sysUserInfo.UserRemark = _aspNetUser.Name;

            var user = await _userRepository.AddAsync(sysUserInfo);
            data.success = user.Id > 0;
            if (data.success)
            {
                data.response = user.Id.ObjToString();
                data.msg = "添加成功";
            }

            return data;
        }

        /// <summary>
        /// 更新用户与角色
        /// </summary>
        /// <param name="sysUserInfo"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] UserInputDto userInputDto)
        {
            // 这里使用事务处理

            var data = new MessageModel<string>();
            try
            {
                var transaction=await _userRepository.Transaction.BeginTransactionAsync();

                if (userInputDto != null && userInputDto.Id > 0)
                {
                    if (userInputDto.RIDs.Count > 0)
                    {
                        // 无论 Update Or Add , 先删除当前用户的全部 U_R 关系
                        var usreroles = (await _userRoleRepository.GetByConditionAsync(d => d.UserId == userInputDto.Id)).Select(d => d.Id.ToString()).ToArray();
                        if (usreroles.Count() > 0)
                        {
                            var isAllDeleted = await _userRoleRepository.DeleteByIds(usreroles);
                        }

                        // 然后再执行添加操作
                        var userRolsAdd = new List<UserRole>();
                        userInputDto.RIDs.ForEach(rid =>
                        {
                            userRolsAdd.Add(new UserRole(userInputDto.Id, rid));
                        });

                        await _userRoleRepository.AddRangeAsync(userRolsAdd);

                    }
                    User user = _mapper.Map<User>(userInputDto);
                    var updatUser = await _userRepository.UpdateAsync(user);
                    if (updatUser != null)
                    {
                        data.success = true;
                    }


                    await  _userRepository.Transaction.CommitTransactionAsync(transaction);

                    if (data.success)
                    {
                        data.msg = "更新成功";
                        data.response = user?.Id.ObjToString();
                    }
                }
            }
            catch (Exception e)
            {
                _userRepository.Transaction.RollbackTransaction();
                //_logger.LogError(e, e.Message);
            }

            return data;
        }

        /// <summary>
        /// 删除用户
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
                var userDetail = await _userRepository.GetAsync(id);
                userDetail.TdIsDelete = true;
                var userUpdate = await _userRepository.UpdateAsync(userDetail);
                if (userUpdate!=null)
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
