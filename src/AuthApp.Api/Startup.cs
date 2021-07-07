using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthApp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using AuthApp.Web;
using AuthApp.ServiceExtensions;
using System.Reflection;
using NetCore.AutoRegisterDi;
using AuthApp.Domian.IRepositories;
using Autofac;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AuthApp.Configuration;
using AuthApp.Domian;
using Microsoft.AspNetCore.Identity;
using AuthApp.CustomerMiddlewares;
using AuthApp.Extensions;
using AuthApp.Api.HttpContextUser;
using Microsoft.AspNetCore.Authorization;
using AuthApp.Api.Permissions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AuthApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    //忽略循环引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //不使用驼峰样式的key
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //设置年月日时分秒时间格式  
                    //options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

            services.AddHttpContextAccessor();

            //context 池
            services.AddDbContextPool<AppDbContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString(AuthAppConsts.ConnectionStringName)
                );
            });

            services.AddAutoMapperSetup();

            #region 进行依赖注入服务的容器
            services.AddScoped<IAspNetUser, AspNetUser>();
            //services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddDIService();
            services.AddAuthorizationSetup();
            services.AddDIRepository();

            #endregion 进行依赖注入服务的容器

            //services.AddAuthentication_JWTSetup();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthApp", Version = "v1" });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthApp v1"));
            }
            app.UseAuthentication();//进入mvc前进行鉴权,UseRouting()中间件之前添加认证中间件
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
