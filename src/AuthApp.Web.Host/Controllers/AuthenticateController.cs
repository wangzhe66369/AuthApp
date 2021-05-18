using AuthApp.Authorizations;
using AuthApp.Configuration;
using AuthApp.Domian;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Web.Host.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly JwtConfig _jwtConfig;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public AuthenticateController(
                UserManager<User> userManager,
                RoleManager<Role> roleManager,
                IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        /// <summary>
        /// 创建用户，ASP.NET Core Identity会处理所有创建失败的情况，如用户名已经存在、密码不符合要求，如果满足所有要求，则用户创建成功。
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        [HttpPost("register", Name = nameof(AddUserAsync))]
        public async Task<IActionResult> AddUserAsync(RegisterUser registerUser)
        {
            var user = new User
            {
                Email = registerUser.Email,
                UserName = registerUser.UserName,
                BirthDate = registerUser.BirthDate
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                ModelState.AddModelError("Error", result.Errors.FirstOrDefault()?.Description);
                return BadRequest(ModelState);
            }
        }

        [HttpPost("token2", Name = nameof(GenerateTokenAsync))]
        public async Task<IActionResult> GenerateTokenAsync(LoginUser loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.UserName);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = _userManager.PasswordHasher.VerifyHashedPassword(user,
                user.PasswordHash,
                loginUser.Password);
            if (result != PasswordVerificationResult.Success)
            {
                return Unauthorized();
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var roleItem in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, roleItem));
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            claims.AddRange(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var signCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(3),
                signingCredentials: signCredential);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = TimeZoneInfo.ConvertTimeFromUtc(jwtToken.ValidTo, TimeZoneInfo.Local)
            });
        }

        [HttpPost("token", Name = nameof(GenerateToken))]
        public IActionResult GenerateToken(LoginUser loginUser)
        {
            if (loginUser.UserName != "demouser"
                || loginUser.Password != "demopassword")
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,loginUser.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var signCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(3),
                signingCredentials: signCredential);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = TimeZoneInfo.ConvertTimeFromUtc(jwtToken.ValidTo, TimeZoneInfo.Local)
            });
        }
    }
}