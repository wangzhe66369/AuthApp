using AuthApp.Api.Permissions;
using AuthApp.Api.Permissions;
using AuthApp.Domian.Identity;
using AuthApp.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VCrisp;
using VCrisp.Data;
using VCrisp.UI;
using VCrisp.Utilities.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthApp.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly IUserRepository _userRepository;
        readonly IUserRoleRepository _userRoleRepository;
        readonly IRoleRepository _roleRepository;
        readonly PermissionRequirement _requirement;
        readonly IUserService _userService;
        private readonly IRoleModulePermissionRepository _roleModulePermissionRepository;


        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="sysUserInfoRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="requirement"></param>
        /// <param name="roleModulePermissionRepository"></param>
        public LoginController(IUserRepository userRepository, 
            IUserRoleRepository userRoleRepository, 
            IRoleRepository roleRepository,
            PermissionRequirement requirement,
            IRoleModulePermissionRepository roleModulePermissionRepository,
            IUserService userService
            )
        {
            this._userRepository = userRepository;
            this._userRoleRepository = userRoleRepository;
            this._roleRepository = roleRepository;
            _requirement = requirement;
            _roleModulePermissionRepository = roleModulePermissionRepository;
            _userService = userService;
        }


        #region 获取token的第1种方法
        /// <summary>
        /// 获取JWT的方法1
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("Token")]
        //public async Task<MessageModel<string>> GetJwtStr(string name, string pass)
        //{
        //    string jwtStr = string.Empty;
        //    bool suc = false;
        //    //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作

        //    var user = await _sysUserInfoRepository.GetUserRoleNameStr(name, MD5Helper.MD5Encrypt32(pass));
        //    if (user != null)
        //    {

        //        TokenModelJwt tokenModel = new TokenModelJwt { Uid = 1, Role = user };

        //        jwtStr = JwtHelper.IssueJwt(tokenModel);
        //        suc = true;
        //    }
        //    else
        //    {
        //        jwtStr = "login fail!!!";
        //    }

        //    return new MessageModel<string>()
        //    {
        //        success = suc,
        //        msg = suc ? "获取成功" : "获取失败",
        //        response = jwtStr
        //    };
        //}


        /// <summary>
        /// 获取JWT的方法2：给Nuxt提供
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("GetTokenNuxt")]
        //public MessageModel<string> GetJwtStrForNuxt(string name, string pass)
        //{
        //    string jwtStr = string.Empty;
        //    bool suc = false;
        //    //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
        //    //这里直接写死了
        //    if (name == "admins" && pass == "admins")
        //    {
        //        TokenModelJwt tokenModel = new TokenModelJwt
        //        {
        //            Uid = 1,
        //            Role = "Admin"
        //        };

        //        jwtStr = JwtHelper.IssueJwt(tokenModel);
        //        suc = true;
        //    }
        //    else
        //    {
        //        jwtStr = "login fail!!!";
        //    }
        //    var result = new
        //    {
        //        data = new { success = suc, token = jwtStr }
        //    };

        //    return new MessageModel<string>()
        //    {
        //        success = suc,
        //        msg = suc ? "获取成功" : "获取失败",
        //        response = jwtStr
        //    };
        //}
        #endregion



        /// <summary>
        /// 获取JWT的方法3：整个系统主要方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("JWTToken3.0")]
        public async Task<MessageModel<TokenInfoViewModel>> GetJwtToken3(string name = "", string pass = "")
        {
            string jwtStr = string.Empty;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pass))
                return new MessageModel<TokenInfoViewModel>()
                {
                    success = false,
                    status = 500,
                    msg = "用户名或密码不能为空",
                    response = default,
                };

            pass = MD5Helper.MD5Encrypt32(pass);

            var user = await _userRepository.GetByConditionAsync(d => d.UserLoginName == name && d.UserLoginPWD == pass && d.TdIsDelete == false);
            if (user.Count() > 0)
            {
                var userRoles = await _userService.GetUserRoleNameStr(name, pass);
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(JwtRegisteredClaimNames.Jti, user.FirstOrDefault().Id.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
                claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));


                // ids4和jwt切换
                // jwt
                //if (!Permissions.IsUseIds4)
                //{
                var data = await _roleModulePermissionRepository.RoleModuleMaps();
                var data1 = data.ToList();
                var list = (from item in data1
                            where item.IsDeleted == false
                                orderby item.Id
                                select new PermissionItem
                                {
                                    Url = item.Module!=null? item.Module.LinkUrl:null,

                                    Role = item.Role!=null? item.Role.RoleName.ObjToString():null,
                                    //Role = item.Role.RoleName.ObjToString(),
                                }).ToList();

                    _requirement.Permissions = list;
                //}

                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                //return Success(token, "获取成功");
                return new MessageModel<TokenInfoViewModel>()
                {
                    success = true,
                    msg = "获取成功",
                    response = token,
                };
            }
            else
            {

                return new MessageModel<TokenInfoViewModel>()
                {
                    success = false,
                    status = 500,
                    msg = "认证失败",
                    response = default,
                };
                
            }
        }

        /// <summary>
        /// 请求刷新Token（以旧换新）
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("RefreshToken")]
        //public async Task<MessageModel<TokenInfoViewModel>> RefreshToken(string token = "")
        //{
        //    string jwtStr = string.Empty;

        //    if (string.IsNullOrEmpty(token))
        //        return Failed<TokenInfoViewModel>("token无效，请重新登录！");
        //    var tokenModel = JwtHelper.SerializeJwt(token);
        //    if (tokenModel != null && tokenModel.Uid > 0)
        //    {
        //        var user = await _sysUserInfoRepository.QueryById(tokenModel.Uid);
        //        if (user != null)
        //        {
        //            var userRoles = await _sysUserInfoRepository.GetUserRoleNameStr(user.uLoginName, user.uLoginPWD);
        //            //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
        //            var claims = new List<Claim> {
        //            new Claim(ClaimTypes.Name, user.uLoginName),
        //            new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ObjToString()),
        //            new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
        //            claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

        //            //用户标识
        //            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
        //            identity.AddClaims(claims);

        //            var refreshToken = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
        //            return Success(refreshToken, "获取成功");
        //        }
        //    }
        //    return Failed<TokenInfoViewModel>("认证失败！");
        //}

        /// <summary>
        /// 获取JWT的方法4：给 JSONP 测试
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="id"></param>
        /// <param name="sub"></param>
        /// <param name="expiresSliding"></param>
        /// <param name="expiresAbsoulute"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("jsonp")]
        //public void Getjsonp(string callBack, long id = 1, string sub = "Admin", int expiresSliding = 30, int expiresAbsoulute = 30)
        //{
        //    TokenModelJwt tokenModel = new TokenModelJwt
        //    {
        //        Uid = id,
        //        Role = sub
        //    };

        //    string jwtStr = JwtHelper.IssueJwt(tokenModel);

        //    string response = string.Format("\"value\":\"{0}\"", jwtStr);
        //    string call = callBack + "({" + response + "})";
        //    Response.WriteAsync(call);
        //}


        /// <summary>
        /// 测试 MD5 加密字符串
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Md5Password")]
        public string Md5Password(string password = "")
        {
            return MD5Helper.MD5Encrypt32(password);
        }
    }
}