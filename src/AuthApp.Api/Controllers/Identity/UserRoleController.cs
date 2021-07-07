using AuthApp.Domian.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VCrisp.UI;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthApp.Api.Controllers.Identity
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserInfoRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="roleRepository"></param>
        public UserRoleController(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            this._userRepository = userRepository;
            this._userRoleRepository = userRoleRepository;
            this._roleRepository = roleRepository;
        }
        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<User>> AddUser(string loginName, string loginPwd)
        {
            User sysUserInfo = new User(loginName, loginPwd);
            User model = new User();
            var userList = await _userRepository.GetByConditionAsync(a => a.UserLoginName == sysUserInfo.UserLoginName && a.UserLoginPWD == sysUserInfo.UserLoginPWD);
            if (userList.Count() > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                model = await _userRepository.AddAsync(sysUserInfo);
            }
            return new MessageModel<User>()
            {
                success = true,
                msg = "添加成功",
                response = model
            };
        }

        /// <summary>
        /// 新建Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<Role>> AddRole(string roleName)
        {
            Role role = new Role(roleName);
            Role model = new Role();
            var userList = await _roleRepository.GetByConditionAsync(a => a.RoleName == role.RoleName && a.Enabled);
            if (userList.Count() > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                model = await _roleRepository.AddAsync(role);
            }
            return new MessageModel<Role>()
            {
                success = true,
                msg = "添加成功",
                response = model
            };
        }

        /// <summary>
        /// 新建用户角色关系
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<UserRole>> AddUserRole(int uid, int rid)
        {
            UserRole userRole = new UserRole(uid, rid);

            UserRole model = new UserRole();
            var userList = await _userRoleRepository.GetByConditionAsync(a => a.UserId == userRole.UserId && a.RoleId == userRole.RoleId);
            if (userList.Count() > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                 model= await _userRoleRepository.AddAsync(userRole);
            }
            return new MessageModel<UserRole>()
            {
                success = true,
                msg = "添加成功",
                response = model
            };
        }

    }
}
