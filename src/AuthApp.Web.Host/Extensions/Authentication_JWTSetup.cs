using AuthApp.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Extensions
{
    public static class Authentication_JWTSetup
    {
        /// <summary>
        /// 当服务器验证Token通过时，JwtBearer认证处理器会通过JwtSecurityTokenHandler将Token转换为ClaimsPrincipal对象，
        /// 并将它赋给HttpContext对象的User属性。ClaimsPrincipal类代表一个用户，它包含一些重要的属性（如Identity和Identities），
        /// 它们分别返回该对象中主要的ClaimsIdentity对象和所有的ClaimsIdentity对象集合。ClaimsIdentity类则代表用户的一个身份，
        /// 一个用户可以有一个或多个身份；ClaimsIdentity类则又由一个或多个Claim组成。Claim类代表与用户相关的具体信息（如用户名和出生日期等），
        /// 该类有两个重要的属性——Type和Value，它们分别表示Claim类型和它的值，它们的类型都是字符串；在ClaimTypes类中定义了一些常见的Claim类型名称。
        /// ClaimsPrincipal、ClaimsIdentity、Claim和ClaimTypes这些类均位于System.Security.Claims命名空间中。
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthentication_JWTSetup(this IServiceCollection services)
        {
            var configuration = (IConfiguration)services.BuildServiceProvider().GetService(typeof(IConfiguration));

            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
            
            var key = Encoding.ASCII.GetBytes(configuration["JwtConfig:Secret"]);

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParams);

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })//指定验证Token时的规则
            .AddJwtBearer(jwt => {
                //jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParams;
            });

        }
    }
}
